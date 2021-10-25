using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public HealthData health; //tracks and edits health
    public ArrayData heartArray; //tracks hearts in scene
    public GameObject heartPrefab, halfHeartPrefab; //heart prefabs
    private GameObject heartFrame, halfHeart, lastHeart; //temps for instantiated hearts
    private int numFrames; //tracks how many hearts
    private bool invincible; //true if hit
    public Animator invincibleAnimation; //player flashes
    public IntData lastHeartSlot; //what array slot holds rightmost heart
    private CamMover cam; //shakes cam
    private AudioSource aSource; //plays damage sounds
    public AudioClip lowSound, hurtSound; //damage sounds
    private bool coroRunning;
    
    void Start()
    { //spawns hearts
        aSource = GetComponent<AudioSource>();
        cam = GetComponentInChildren<CamMover>();
        health.health = 10;
        health.maxHealth = heartArray.array.Length;
        lastHeartSlot.value = health.health;
        heartArray.Clear();
        numFrames = health.health / 2; //checks if health is odd or even
        if (health.health % 2 == 1)
        {
            numFrames++;
        }
        int z = 0;
        for (int i = 0; i < numFrames; i++)
        { //
            heartFrame = Instantiate(heartPrefab, new Vector3((i + 1) * 120,-120,0), Quaternion.identity);
            heartFrame.transform.SetParent(GameObject.FindGameObjectWithTag("Hearts").transform, false);
            if (i < numFrames)
            {
                halfHeart = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
                halfHeart.transform.SetParent(heartFrame.transform, false);
            }
            else if (numFrames * 2 == health.health && i == numFrames - 1)
            {
                halfHeart = Instantiate(halfHeartPrefab, new Vector3(50, -50, 0), Quaternion.identity);
                halfHeart.transform.SetParent(heartFrame.transform, false);
            }
            if (heartFrame != null)
            {
                heartArray.array[z] = heartFrame;
                heartFrame = null;
                z++;
            }
            if (halfHeart != null)
            {
                heartArray.array[z] = halfHeart;
                halfHeart = null;
                z++;
            }
        }
    }

    private void Update()
    {
        if (!coroRunning)
        {
            if (health.health > 0 && health.health <= 2)
            {
                StartCoroutine(LowHealth(health.health));
            }
            else if (health.health == 0)
            {
                //Death
            }
        }
    }

    private void OnTriggerStay(Collider other)
    { //damages player when touching enemy or their bullets
        if (!invincible)
        {
            if (other.CompareTag("Enemy"))
            {
                AIDefault enemy = other.GetComponent<AIDefault>();
                StartCoroutine(Hurt(enemy.touchDamage));
            
            }
            else if (other.CompareTag("EnemyBullet"))
            {
                Bullet eBullet = other.GetComponent<Bullet>();
                StartCoroutine(Hurt(eBullet.gun.bulletDamage));
            }
        }
    }

    IEnumerator Hurt(int damage)
    { //hurts player, plays animations
        invincible = true;
        aSource.clip = hurtSound;
        aSource.Play();
        health.Damage(damage);
        heartArray.GetLast();
        Destroy(heartArray.array[lastHeartSlot.value]);
        cam.Shake(5, 0.1f);
        invincibleAnimation.SetBool("Invincible", true);
        yield return new WaitForSeconds(3f);
        invincibleAnimation.SetBool("Invincible", false);
        invincible = false;
    }

    IEnumerator LowHealth(int health)
    {
        coroRunning = true;
        aSource.clip = lowSound;
        aSource.Play();
        yield return new WaitForSeconds(health * 0.75f);
        coroRunning = false;
    }
}