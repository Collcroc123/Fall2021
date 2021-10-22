using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GunData gun; //current gun
    private Rigidbody rbody; //bullet's rigidbody
    private GameObject bulletSpawn; //location where bullet spawns
    public SpriteRenderer texture; //bullet's texture
    public AudioSource source; //gunshot sound
    public StatsData stats; //tracks statistics
    public AudioClip hitSound;
    
    void Start()
    { //gives bullet attributes depending on what gun is fired
        rbody = GetComponent<Rigidbody>();
        bulletSpawn = GameObject.Find("BulletSpawn");
        gun = bulletSpawn.GetComponent<GunManager>().gun;
        texture.material = gun.bulletTexture;
        rbody.velocity = bulletSpawn.transform.forward * gun.bulletSpeed;
        source.clip = gun.gunshot.soundArray[Random.Range(0, gun.gunshot.soundArray.Length - 1)];
        source.Play();
        stats.bulletsFired++;
    }

    private void OnTriggerEnter(Collider other)
    { //checks if bullet hits something
        if (other.CompareTag("Enemy"))
        {
            EnemyManager enemy = other.GetComponent<EnemyManager>();
            enemy.health -= gun.bulletDamage;
            Hit();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Crate") || other.CompareTag("Door"))
        {
            Hit();
        }
    }

    private void OnTriggerExit(Collider other)
    { //deletes bullet if it hits nothing after 5 secs
        StartCoroutine(WaitFor(5));
    }

    private IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    private void Hit()
    {
        source.clip = hitSound;
        source.Play();
        texture.enabled = false;
        StartCoroutine(WaitFor(0.5f));
    }
}