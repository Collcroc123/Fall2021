using System.Collections;
using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    private GameManager manager; // Checks if level is done generating
    private Animator doorAnim; // Opens and closes doors
    private MeshFilter mesh; // Doorframe's mesh
    private MeshCollider meshColl; // Doorframe collider
    //public GameObject colliderObject;
    private BoxCollider boxCollider; // Checks if door leads to wall
    private bool door, done; // Keeps door from constantly filling
    public GameObject lDoor, rDoor; // Door objects
    public SpriteRenderer mapDoor; // Door on minimap
    public Mesh wall; // Replaces doorframe if leading to wall
    
    private void Start()
    { // door = false; done = false;
        manager = GameObject.Find("/Manager").GetComponent<GameManager>();
        doorAnim = GetComponent<Animator>();
        mesh = GetComponent<MeshFilter>();
        meshColl = GetComponent<MeshCollider>();
        boxCollider = GetComponent<BoxCollider>(); //colliderObject.
        
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
        else if (other.CompareTag("Wall"))
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
        else if (other.CompareTag("Wall"))
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
        Destroy(lDoor); 
        Destroy(rDoor);
        mesh.mesh = wall;
        meshColl.sharedMesh = wall;
        gameObject.transform.tag = "Wall";
        mapDoor.color = new Color(255, 255, 255, 255);
        boxCollider.enabled = false;
        //Destroy(colliderObject);
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