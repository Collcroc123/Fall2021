using System.Collections;
using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    private RoomLister list;
    private Animator doorAnim;
    private MeshFilter mesh;
    private BoxCollider boxCollider;
    private bool door;
    public bool done; //If done=true, keeps door from constantly filling
    public GameObject lDoor, rDoor;
    public Mesh wall;
    
    private void Start()
    {
        door = false;
        done = false;
        print("Number of doors pre-done: ");
        list = GameObject.Find("/Manager").GetComponent<RoomLister>();
        doorAnim = GetComponent<Animator>();
        mesh = GetComponent<MeshFilter>();
        boxCollider = GetComponent<BoxCollider>();
        
    }

    private void Update()
    {
        if (list.levelDone && !done) //WHY IS IT NOT DETECTING NO DOOR SOMETIMES?
        {
            StartCoroutine(waitDoor());
            print("Number of doors post-done: ");
            done = true;
            if (!door)
            {
                print("Doors filled: ");
                FillHole();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door")) { door = true; }
        else if (other.CompareTag("Wall")) { door = false; }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door")) { door = true; }
        else if (other.CompareTag("Wall")) { door = false; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door")) { door = false; }
    }

    void FillHole()
    {
        Destroy(lDoor); 
        Destroy(rDoor); 
        mesh.mesh = wall; 
        boxCollider.enabled = false;
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
    {
        Vector3 pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(pos.x, 10, pos.z);
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = pos;
        
    }
}