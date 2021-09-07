using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    private RoomLister list;
    private Animator doorAnim;
    private MeshFilter mesh;
    private Collider boxCollider;
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
            //boxCollider.enabled = false;
            //boxCollider.enabled = true;
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door")) { door = true; }
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
}