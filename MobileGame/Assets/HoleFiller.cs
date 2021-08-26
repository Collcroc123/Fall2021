using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleFiller : MonoBehaviour
{
    public Mesh wall;
    public GameObject lDoor;
    public GameObject rDoor;
    private MeshFilter mesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            print("Wall Detected!");
            Destroy(lDoor);
            Destroy(rDoor);
            mesh.mesh = wall;
        }
        else if (other.CompareTag("Player"))
        {
            
        }
    }
}
