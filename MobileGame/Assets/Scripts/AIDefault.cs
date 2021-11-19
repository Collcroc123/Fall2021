using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class AIDefault : MonoBehaviour
{
    public GameManager manager;
    [HideInInspector] public HealthData health;
    private RoomManager roomMan;
    private NavMeshAgent agent;
    private GunManager gunMan;
    private Vector3 startPos;
    private bool moving;
    public GameObject player, sprite, deathAnim;
    public int enemyHealth,touchDamage, patrolRange, points;
    public float waitBeforeMove, moveSpeed;
    
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        roomMan = GetComponentInParent<RoomManager>();
        health = ScriptableObject.CreateInstance<HealthData>();
        gunMan = GetComponentInChildren<GunManager>();
        agent = GetComponent<NavMeshAgent>();
        health.health = enemyHealth;
        startPos = transform.position;
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (player != null)
        { //Looks at player, shoots
            Vector3 relativePos = player.transform.position - gameObject.transform.position; relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            sprite.transform.rotation = rotation;
            if (!gunMan.isShooting && roomMan.playerEntered && manager.canShoot)
            { //Won't shoot while on cooldown or if player isn't in room
                gunMan.Shoot();
            }
        }
        if (health.health <= 0)
        {
            manager.AddPoints(points);
            manager.stats.enemiesKilled++;
            Instantiate(deathAnim, gameObject.transform.position, Quaternion.Euler(90, 0 ,0));
            roomMan.enemyNum--;
            Destroy(gameObject);
        }
    }
    IEnumerator Patrol()
    {
        yield return new WaitForSeconds(1f);
        moving = true;
        agent.speed = moveSpeed;
        while (moving)
        {
            yield return new WaitForFixedUpdate();
            if (agent.pathPending || !(agent.remainingDistance < 0.5f)) continue;
            //yield return new WaitForSeconds(waitBeforeMove);
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            agent.destination = (Random.insideUnitSphere * patrolRange) + startPos;
            if (roomMan.playerEntered) //Enemy won't move until player enters
            {
                //agent.destination = (Random.insideUnitSphere * patrolRange) + startPos;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            health.health -= other.GetComponent<Bullet>().gun.bulletDamage;
            manager.stats.damageDealt += other.GetComponent<Bullet>().gun.bulletDamage;
        }
    }
}