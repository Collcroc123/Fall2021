using System.Collections;
using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    private GameManager manager; //checks if level is done generating
    private Animator doorAnim; //opens and closes doors
    private MeshFilter mesh; //doorframe's mesh
    private BoxCollider boxCollider; //checks if door leads to wall
    private bool door, done; //keeps door from constantly filling
    public GameObject lDoor, rDoor; //door objects
    public SpriteRenderer mapDoor; //door on minimap
    public Mesh wall; //replaces doorframe if leading to wall
    
    private void Start()
    { //door = false; done = false;
        manager = GameObject.Find("/Manager").GetComponent<GameManager>();
        doorAnim = GetComponent<Animator>();
        mesh = GetComponent<MeshFilter>();
        boxCollider = GetComponent<BoxCollider>();
        
    }

    private void Update()
    {
        if (manager.levelDone && !done)
        { //prevents running more than once after level gen
            StartCoroutine(waitDoor());
            done = true;
            if (!door)
            {
                FillHole();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { //sets door true if this door connects to another
        if (other.CompareTag("Doorframe"))
        {
            door = true;
        }
        else if (other.CompareTag("Wall"))
        {
            door = false;
        }
    }

    private void OnTriggerStay(Collider other)
    { //sets door true if this door connects to another
        if (other.CompareTag("Doorframe"))
        {
            door = true;
        }
        else if (other.CompareTag("Wall"))
        {
            door = false;
        }
    }

    private void OnTriggerExit(Collider other)
    { //sets door false if other door no longer seen (deleted in gen)
        if (other.CompareTag("Doorframe"))
        {
            door = false;
        }
    }

    void FillHole()
    { //replaces doorframe with wall
        Destroy(lDoor); 
        Destroy(rDoor);
        mesh.mesh = wall;
        gameObject.transform.tag = "Wall";
        mapDoor.color = new Color(255, 255, 255, 255);
        boxCollider.enabled = false;
        print("Doors filled: ");
    }

    public void OpenDoor()
    {
        doorAnim.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        doorAnim.SetBool("Open", false);
    }

    IEnumerator waitDoor()
    { //moves & toggles doors after gen to register their door status
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