using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    public Mesh wall;
    public GameObject lDoor, rDoor;
    private MeshFilter mesh;
    private bool door = false;
    private Animator doorAnim;

    private void Start()
    {
        doorAnim = GetComponent<Animator>();
        mesh = GetComponent<MeshFilter>();
        Invoke("FillHole", 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door")) { door = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door")) { door = false; }
    }

    void FillHole()
    {
        if (!door)
        { 
            print("Hole Detected"); 
            Destroy(lDoor); 
            Destroy(rDoor); 
            mesh.mesh = wall;
        }
    }

    public void OpenDoor()
    {
        print("Opened!");
        doorAnim.SetBool("Open", true);
    }
    
    public void CloseDoor()
    {
        print("Closed!");
        doorAnim.SetBool("Open", false);
    }
}