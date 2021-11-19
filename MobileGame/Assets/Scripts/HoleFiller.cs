using System.Collections;
using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    private GameManager manager; // Checks if level is done generating
    public GameObject doorFrame, wallPrefab; // Frame and Wall objects
    private bool door, done; // Keeps door from constantly filling
    private BoxCollider boxCollider; // Detects if door touches wall
    
    private void Start()
    {
        manager = GameObject.Find("/Manager").GetComponent<GameManager>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (manager.levelDone && !done)
        { // Prevents running more than once after level gen
            StartCoroutine(waitDoor());
            done = true;
            if (!door)
            {
                FillHole();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { // Sets door true if this door connects to another
        if (other.CompareTag("Doorframe"))
        {
            door = true;
        }
        if (other.CompareTag("Wall"))
        {
            door = false;
        }
    }

    private void OnTriggerStay(Collider other)
    { // Sets door true if this door connects to another
        if (other.CompareTag("Doorframe"))
        {
            door = true;
        }
        if (other.CompareTag("Wall"))
        {
            door = false;
        }
    }

    private void OnTriggerExit(Collider other)
    { // Sets door false if other door no longer seen (deleted in gen)
        if (other.CompareTag("Doorframe"))
        {
            door = false;
        }
    }

    void FillHole()
    { // Replaces doorframe with wall
        Destroy(doorFrame);
        Instantiate(wallPrefab, gameObject.transform);
        print("Doors filled: ");
        //Destroy(gameObject);
    }

    IEnumerator waitDoor()
    { // Moves & toggles doors after gen to register their door status
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(pos.x, -10, pos.z);
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = pos;
    }
}