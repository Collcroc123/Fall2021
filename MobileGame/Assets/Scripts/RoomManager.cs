using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Animator roomFade; //black that covers outside rooms
    public HoleFiller door1, door2, door3, door4; //all doors in the room
    private OLDMAPGEN mapGen; //sets spawn room as complete
    public bool roomComplete, playerEntered; //no enemies in room, opens doors
    private bool done; //keeps door from ever opening or closing
    public int enemyNum;
    public GameObject cratePrefab;
    public IntData roomsSinceLastCrate;
    public Light lightOne, lightTwo;

    void Start()
    {
        //lightOne = GetComponentInChildren<Light>();
        //lightTwo = GetComponentInChildren<Light>();
        //roomComplete = true; //TEMP TRUE WITHOUT ENEMIES
        mapGen = GetComponentInParent<OLDMAPGEN>();
        if (mapGen.isSpawn) { roomComplete = true; }
        Invoke(nameof(Open), 0.5f);
    }

    private void Update()
    {
        if (enemyNum <= 0)
        {
            roomComplete = true;
        }
        if (roomComplete && !done)
        {
            done = true;
            if (roomsSinceLastCrate.value >= 5)
            {
                if (Random.Range(0, 5) >= 2.5f)
                {
                    Instantiate(cratePrefab, gameObject.transform.position, Quaternion.identity);
                    roomsSinceLastCrate.value = 0;
                }
            }
            Invoke(nameof(Open), 0.5f);
        }
    }
    
    private void Open()
    {
        if (door1 != null) 
            door1.OpenDoor();
        if (door2 != null) 
            door2.OpenDoor();
        if (door3 != null) 
            door3.OpenDoor();
        if (door4 != null) 
            door4.OpenDoor();
    }

    private void Close()
    {
        if (door1 != null) 
            door1.CloseDoor();
        if (door2 != null) 
            door2.CloseDoor();
        if (door3 != null) 
            door3.CloseDoor();
        if (door4 != null) 
            door4.CloseDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //lightOne.enabled = true;
            //lightTwo.enabled = true;
            StartCoroutine(Wait());
            playerEntered = true;
            roomFade.SetBool("Enter", true);
            if (!roomComplete) 
                Close();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //lightOne.enabled = false;
            //lightTwo.enabled = false;
            playerEntered = false;
            roomFade.SetBool("Enter", false);
        }
        else if (other.CompareTag("Enemy")) 
            roomComplete = true;
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}