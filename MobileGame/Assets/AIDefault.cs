using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class AIDefault : MonoBehaviour
{
    public HealthData health; //[HideInInspector] 
    private NavMeshAgent agent;
    private Vector3 startPos;
    private bool moving, isShooting;
    public float waitBeforeMove, moveSpeed;
    public int enemyHealth, patrolRange;
    public GameObject player, sprite, bulletSpawn, deathAnim;
    public GunData gun;
    private RoomManager roomMan;
    public int touchDamage = 1;

    void Start()
    {
        roomMan = GetComponentInParent<RoomManager>();
        health = ScriptableObject.CreateInstance<HealthData>();
        health.health = enemyHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        Vector3 relativePos = player.transform.position - gameObject.transform.position; relativePos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        sprite.transform.rotation = rotation;
        if (!isShooting && roomMan.playerEntered)
        {
            StartCoroutine(Shoot());
        }

        if (health.health <= 0)
        {
            Instantiate(deathAnim, gameObject.transform.position, Quaternion.identity);
            roomMan.enemyNum--;
            Destroy(gameObject);
        }
    }

    public IEnumerator Patrol()
    {
        yield return new WaitForSeconds(1f);
        moving = true;
        agent.speed = moveSpeed;
        while (moving)
        {
            yield return new WaitForFixedUpdate();
            if (agent.pathPending || !(agent.remainingDistance < 0.5f)) continue;
            yield return new WaitForSeconds(waitBeforeMove);
            if (roomMan.playerEntered)
            {
                agent.destination = (Random.insideUnitSphere * patrolRange) + startPos;
            }
        }
    }
    
    IEnumerator Shoot()
    { //shoots bullet(s)
        isShooting = true;
        Bullet bullet = Instantiate(gun.bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation).GetComponent<Bullet>();
        bullet.bulletSpawn = bulletSpawn;
        yield return new WaitForSeconds(gun.fireRate);
        isShooting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet pBullet = other.GetComponent<Bullet>();
            health.health -= pBullet.gun.bulletDamage;
            print(health.health);
        }
    }
}