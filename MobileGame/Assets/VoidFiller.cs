using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidFiller : MonoBehaviour
{
    public Mesh wall;
    public GameObject lDoor;
    public GameObject rDoor;
    public MeshFilter mesh;
    private bool floor = false;
    
    //public MapGenerator mapGen;

    private void Start()
    {
        Invoke("FillVoid", 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            floor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        floor = false;
    }

    void FillVoid()
    {
        if (!floor)
        { 
            print("Void Detected!"); 
            Destroy(lDoor); 
            Destroy(rDoor); 
            mesh.mesh = wall;
        }
    }
}