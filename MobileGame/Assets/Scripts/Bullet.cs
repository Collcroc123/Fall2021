using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public GameObject bulletSpawn; //location where bullet spawns
    [HideInInspector] public GunData gun; //current gun
    private Rigidbody rbody; //bullet's rigidbody
    public SpriteRenderer texture; //bullet's texture
    public AudioSource source; //gunshot sound
    public StatsData stats; //tracks statistics
    //private bool hit; //unsure?
    public bool isEnemyBullet; //tells if bullet is fired by player
    public GameObject hitAnim; //sound and particles
    
    void Start()
    { //gives bullet attributes depending on what gun is fired
        rbody = GetComponent<Rigidbody>();
        gun = bulletSpawn.GetComponent<GunManager>().gun;
        texture.material = gun.bulletTexture;
        rbody.velocity = bulletSpawn.transform.forward * gun.bulletSpeed;
        source.clip = gun.gunshot.soundArray[Random.Range(0, gun.gunshot.soundArray.Length - 1)];
        source.Play();
        if (!isEnemyBullet)
        {
            stats.bulletsFired++;
        }
    }

    private void OnTriggerEnter(Collider other)
    { //checks if bullet hits something
        if (other.CompareTag("Enemy") && !isEnemyBullet)
        {
            Hit();
        }
        else if (other.CompareTag("Player") && isEnemyBullet)
        {
            Hit();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Door"))
        {
            Hit();
        }
        else if (other.CompareTag("Crate"))
        {
            //Do Item Stuff Here!
            Hit();
        }
    }
    
    private void Hit()
    {
        Instantiate(hitAnim, gameObject.transform.position, Quaternion.identity);
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