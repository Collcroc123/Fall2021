using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GunData gun;
    private Rigidbody rbody;
    private GameObject player;
    public SpriteRenderer texture;
    void Start()
    {
        player = GameObject.Find("BulletSpawn");
        gun = player.GetComponent<GunManager>().gun;
        texture.material = gun.bulletTexture;
        rbody = GetComponent<Rigidbody>();
        rbody.velocity = player.transform.forward * gun.bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyManager enemy = other.GetComponent<EnemyManager>();
            enemy.health -= gun.bulletDamage;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Crate"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //waitFor(3);
        //Destroy(gameObject);
    }

    private IEnumerator waitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}