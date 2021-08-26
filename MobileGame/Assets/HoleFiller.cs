using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    public Mesh wall;
    public GameObject lDoor;
    public GameObject rDoor;
    private MeshFilter mesh;
    private bool door = false;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>();
        Invoke("FillHole", 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            door = false;
        }
    }
    
    void FillHole()
    {
        if (!door)
        { 
            print("Hole Detected!"); 
            Destroy(lDoor); 
            Destroy(rDoor); 
            mesh.mesh = wall;
        }
    }
}