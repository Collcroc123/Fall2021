using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private ObjectData gun;
    private Rigidbody rbody;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("BulletSpawn");
        gun = player.GetComponent<PlayerMove>().gun;
        rbody = GetComponent<Rigidbody>();
        rbody.velocity = (player.transform.forward * gun.bulletSpeed);
    }

    private void Update()
    {
        //rbody.MovePosition(player.transform.forward * gun.bulletSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyManager enemy = other.GetComponent<EnemyManager>();
            enemy.health -= gun.bulletDamage;
            Destroy(gameObject);
        }
    }
}