using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public int directionNum; //1234 = SWNE
    public ArrayData northRooms, eastRooms, southRooms, westRooms;
    private bool roomGenerated;
    private int randomizer;
    public bool isSpawn = false;
    private bool isEnd = false;
    public IntData totalRooms;
    private int maxRooms = 50;
    //[HideInInspector] public bool touching = false;

    void Start()
    {
        if (isSpawn)
        {
            randomizer = Random.Range(1, 7);
            directionNum = Random.Range(1, 4);
            Invoke("Generate", 0.05f);
        }
        else
        {
            randomizer = Random.Range(0, 7);
            Invoke("Generate", 0.05f);
        }
    }

    void Generate()
    {
        if (!roomGenerated && totalRooms.value <= maxRooms)
        {
            if (totalRooms.value == maxRooms)
            {
                isEnd = true;
            }
            if (directionNum == 1)
            {
                var newRoom = Instantiate(southRooms.array[randomizer], gameObject.transform.position, southRooms.array[randomizer].transform.rotation);
                //newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 2)
            {
                var newRoom = Instantiate(westRooms.array[randomizer], gameObject.transform.position, westRooms.array[randomizer].transform.rotation);
                //newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 3)
            {
                var newRoom = Instantiate(northRooms.array[randomizer], gameObject.transform.position, northRooms.array[randomizer].transform.rotation);
                //newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 4)
            {
                var newRoom = Instantiate(eastRooms.array[randomizer], gameObject.transform.position, eastRooms.array[randomizer].transform.rotation);
                //newRoom.transform.parent = gameObject.transform;
            }
            
            totalRooms.value++;
            roomGenerated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room") && other.GetComponent<MapGenerator>().roomGenerated)
        {
            Destroy(gameObject);
            print("Destroyed due to overlap with existing wall!");
            /*WHERE YOU LEFT OFF LAST NIGHT:
             Doors to the void generate when room cap is hit
             Somehow you need to detect when the map is done generating
             Find the doors to the void and use HoleFiller
            */
        }
    }
}