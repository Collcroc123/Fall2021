using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager manager;
    [HideInInspector] public GunData gun; // Current gun
    [HideInInspector] public GameObject bulletSpawn; // Location where bullet spawns
    private Renderer texture; // Bullet's texture
    private Rigidbody rbody; // Bullet's rigidbody
    private AudioSource source; // Gunshot sound
    public GameObject hitAnim; // Sound and particles
    // Private Light bulletLight;
    
    void Start()
    { // Gives bullet attributes depending on what gun is fired
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        texture = GetComponentInChildren<SpriteRenderer>();
        // bulletLight = GetComponentInChildren<Light>();
        source = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody>();
        texture.material.color = gun.gunColor;
        rbody.velocity = bulletSpawn.transform.forward * gun.bulletSpeed;
        // source.clip = gun.gunshot[Random.Range(0, gun.gunshot.Length - 1)];
        source.clip = gun.gunshot[0];
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
        if (gameObject.CompareTag("Bullet"))
            manager.stats.bulletsFired++;
    }

    private void OnTriggerEnter(Collider other)
    { // Checks if bullet hits something
        if (other.CompareTag("Enemy") && gameObject.CompareTag("Bullet"))
        {
            Hit();
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("EnemyBullet"))
        {
            Hit();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Door") || other.CompareTag("Crate"))
        {
            Hit();
        }
    }
    
    private void Hit()
    {
        Instantiate(hitAnim, gameObject.transform.position, Quaternion.Euler(90, 0 ,0));
        Destroy(gameObject);
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
}