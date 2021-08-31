using System;
using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    private RoomLister list;
    public Mesh wall;
    public GameObject lDoor, rDoor;
    private MeshFilter mesh;
    private bool door = false;
    private Animator doorAnim;
    private bool done; //If done=true, keeps door from constantly filling

    private void Start()
    {
        list = GameObject.Find("/Manager").GetComponent<RoomLister>();
        doorAnim = GetComponent<Animator>();
        mesh = GetComponent<MeshFilter>();
        //Invoke(nameof(FillHole), 3f);
    }

    private void Update()
    {
        if (list.levelDone && !done)
        {
            done = true;
            FillHole();
        }
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