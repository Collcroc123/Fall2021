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

        if (other.CompareTag("Player"))
        {
            print("Opened!");
            //doorAnim.SetBool("Close", false);
            doorAnim.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door")) { door = false; }
        
        if (other.CompareTag("Player"))
        {
            print("Closed!");
            doorAnim.SetBool("Open", false);
            //doorAnim.SetBool("Close", true);
            
        }
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
}