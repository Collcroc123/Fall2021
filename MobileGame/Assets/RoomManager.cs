using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public HoleFiller door1, door2, door3, door4;
    private bool roomComplete;
    public MapGenerator mapGen;
    
    
    void Start()
    {
        if (mapGen.isSpawn)
        {
            roomComplete = true;
        }
    }
    
    void Update()
    {
        
    }
    /* TO DO:
     * Make camera snap to center of room smoothly when entered
     * Locate doors connected to room via triggers
     * Close the doors once the player is inside the room (force animation?)
     * Spawn enemies (random enemy, position, and amount)
     * Detect when all enemies are no longer in room (killed)
     * Open all doors
     * Detect items left in rooms (icons on minimap)
     */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !roomComplete)
        {
            door1.CloseDoor();
            door2.CloseDoor();
            door3.CloseDoor();
            door4.CloseDoor();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
