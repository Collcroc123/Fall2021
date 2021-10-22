using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class AIDefault : MonoBehaviour
{
    private WaitForFixedUpdate wffu;
    private WaitForSeconds waitFor;
    private WaitForSeconds focusFor;
    private Vector3 startPos;
    public bool canPatrol = true;
    public bool canNavigate;
    public Transform player;
    public float waitTime = 2f;
    public float focusTime = 1f;
    public float runSpeed = 8f;
    public float patrolSpeed = 4f;
    public int patrolRange = 5;
    public bool seen = false;
    public bool attackMode = false;
    public bool enemyTurn;
    public GameObject damage;
    private bool coroRunning = false;

    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        waitFor = new WaitForSeconds(waitTime);
        focusFor = new WaitForSeconds(focusTime);
        StartCoroutine(Patrol());
    }

    public IEnumerator Navigate()
    {
        coroRunning = true;
        damage.SetActive(true);
        canPatrol = false;
        canNavigate = true;
        yield return focusFor;
        while (canNavigate)
        {
            seen = true;
            agent.speed = runSpeed;
            yield return wffu;
            agent.destination = player.position;
            if (enemyTurn == false && !attackMode)
            {
                coroRunning = false;
                StopCoroutine(Navigate());
                break;
            }
        }
    }

    public IEnumerator Patrol()
    {
        coroRunning = true;
        damage.SetActive(false);
        canPatrol = true;
        canNavigate = false;
        while (canPatrol)
        {
            agent.speed = patrolSpeed;
            yield return wffu;
            if (agent.pathPending || !(agent.remainingDistance < 0.5f)) continue;
            yield return waitFor;
            agent.destination = (Random.insideUnitSphere * patrolRange) + startPos;
            if (enemyTurn == true && !attackMode)
            {
                coroRunning = false;
                StopCoroutine(Patrol());
                break;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPatrol = false;
            canNavigate = false;
            if (attackMode || enemyTurn == true)
            {
                StartCoroutine(Navigate());
            }
            else
            {
                StartCoroutine(Patrol());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!coroRunning && other.gameObject.CompareTag("Player"))
        {
            canPatrol = false;
            canNavigate = false;
            if (enemyTurn == true)
            {
                StartCoroutine(Navigate());
            }
            else
            {
                StartCoroutine(Patrol());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPatrol = false;
            canNavigate = false;
            seen = false;
            StartCoroutine(Patrol());
        }
    }
}
