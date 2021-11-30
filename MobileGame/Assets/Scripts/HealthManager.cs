using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameManager manager; // Game Manager
    private PlayerMove player; // Player
    public HealthData health; // Tracks and edits health
    public ArrayData heartArray; // Tracks hearts in scene
    public GameObject heartFramePrefab, halfHeartPrefab; // Heart prefabs
    private GameObject heartFrameTemp, halfHeartTemp, lastHeart; // Temps for instantiated hearts
    private int numFrames; // Tracks how many heart frames there are
    private bool invincible; // True if player was hit
    public Animator invincibleAnimation; // Player flashes
    private CamMover cam; // Shakes cam
    private AudioSource aSource; // Plays damage sounds + crate
    public AudioClip lowSound, hurtSound, healSound, crateSound; // Damage sounds + crate
    private bool coroRunning; // Makes sure coroutine isn't already running
    public GameObject deathPanel, deathAnim; // Game Over screen
    int z = 0; // Tracks heartArray

    void Start()
    { // Spawns hearts
        player = GetComponent<PlayerMove>();
        aSource = GetComponent<AudioSource>();
        cam = GetComponentInChildren<CamMover>();
        health.maxHealth = heartArray.array.Length;
        heartArray.GetLast();
        heartArray.Clear();
        numFrames = health.health / 2; 
        if (health.health % 2 == 1) // 0 = Even, 1 = Odd
            numFrames++; // Odd doesn't divide cleanly, so add 1
        z = 0;
        for (int i = 0; i < numFrames; i++)
        { // Create frames and fill them
            heartFrameTemp = Instantiate(heartFramePrefab, new Vector3((i + 1) * 120,-120,0), Quaternion.identity);
            heartFrameTemp.transform.SetParent(GameObject.FindGameObjectWithTag("Hearts").transform, false);
            if (i < numFrames)
            { // If not last frame, fill frame
                halfHeartTemp = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
                halfHeartTemp.transform.SetParent(heartFrameTemp.transform, false);
            }
            else if (health.health % 2 == 0 && i == numFrames - 1)
            { // If last frame and health is even, fill frame
                halfHeartTemp = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
                halfHeartTemp.transform.SetParent(heartFrameTemp.transform, false);
            }
            if (heartFrameTemp != null)
            { // Stores Heart Frame in array before clearing it
                heartArray.array[z] = heartFrameTemp;
                if (i < numFrames-1)
                {
                    heartFrameTemp = null;
                }
                z++;
            }
            if (halfHeartTemp != null)
            { // Stores Heart Half in array before clearing it
                heartArray.array[z] = halfHeartTemp;
                if (i < numFrames-1)
                {
                    halfHeartTemp = null;
                }
                z++;
            }
        }
    }

    private void Update()
    {
        if (!coroRunning)
        {
            if (health.health > 0 && health.health <= 2)
            { // Plays warning when only one heart frame left
                StartCoroutine(LowHealth(health.health));
            }
        }
        else if (health.health == 0)
        { // Dead
            coroRunning = true;
            manager.stats.deaths++;
            deathPanel.SetActive(true);
            Instantiate(deathAnim, gameObject.transform.position, Quaternion.Euler(90, 0 ,0));
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerStay(Collider other)
    { 
        if (!invincible)
        { // Damages player when touching enemy or their bullets
            if (other.CompareTag("Enemy"))
            {
                AIDefault enemy = other.GetComponent<AIDefault>();
                StartCoroutine(Hurt(enemy.touchDamage));
            
            }
            else if (other.CompareTag("EnemyBullet"))
            {
                Bullet eBullet = other.GetComponent<Bullet>();
                StartCoroutine(Hurt(1)); // eBullet.gun.bulletDamage
            }
        }
        if (other.CompareTag("Crate"))
        { // Heals player touching crate
            if (other.GetComponent<Crate>().isHeart == true && health.health < health.maxHealth)
            {
                Destroy(other.GetComponent<Crate>().crate);
                StartCoroutine(Heal(1));
            }
            else if (other.GetComponent<Crate>().isHeart == false && !coroRunning)
            {
                StartCoroutine(Wait(other));
                //Destroy(other.gameObject);
            }
        }
    }

    IEnumerator Hurt(int damage)
    { // Hurts player, plays animations
        invincible = true;
        aSource.clip = hurtSound;
        aSource.Play();
        health.Damage(damage);
        manager.stats.damageTaken += damage;
        heartArray.GetLast();
        z--;
        Destroy(heartArray.array[heartArray.lastNum.value]);
        cam.Shake(5, 0.1f);
        invincibleAnimation.SetBool("Invincible", true);
        yield return new WaitForSeconds(2f);
        invincibleAnimation.SetBool("Invincible", false);
        invincible = false;
    }

    IEnumerator Heal(int healing)
    { // Heals player, plays animations
        invincible = true;
        aSource.clip = healSound;
        aSource.Play();
        health.Heal(healing);
        manager.stats.healthGained += healing;
        heartArray.GetLast();
        if (health.health % 2 == 1) // 0 = Even, 1 = Odd
        { // Spawns new heart frame
            heartFrameTemp = Instantiate(heartFramePrefab, new Vector3((heartArray.lastNum.value / 2 + 2) * 120,-120,0), Quaternion.identity);
            heartFrameTemp.transform.SetParent(GameObject.FindGameObjectWithTag("Hearts").transform, false);
            heartArray.array[z] = heartFrameTemp;
        }
        else if (health.health % 2 == 0)
        { // Spawns new heart half
            halfHeartTemp = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
            halfHeartTemp.transform.SetParent(heartArray.array[z-1].transform, false);
            heartArray.array[z] = halfHeartTemp;
        }
        z++;
        invincibleAnimation.SetBool("Invincible", true);
        yield return new WaitForSeconds(1f);
        invincibleAnimation.SetBool("Invincible", false);
        invincible = false;
    }

    IEnumerator LowHealth(int health)
    { // Plays low health sound
        coroRunning = true;
        yield return new WaitForSeconds(health * 0.75f);
        aSource.clip = lowSound;
        aSource.Play();
        coroRunning = false;
    }

    IEnumerator Wait(Collider other)
    {
        coroRunning = true;
        aSource.clip = healSound;
        aSource.Play();
        GunData tempOld = player.gunMan.gun;
        player.gunMan.SetGun(other.GetComponent<Crate>().gun);
        other.GetComponent<Crate>().ReplaceGun(tempOld);
        yield return new WaitForSeconds(1f);
        coroRunning = false;
    }
}