using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class OLDMAPGEN : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public ArrayData northRooms, eastRooms, southRooms, westRooms;
    public IntData totalRooms;
    public bool isSpawn, isEnd;
    public int directionNum;
    private RoomLister allRooms;
    private bool roomGenerated;
    private int randomizer;
    private Vector3 roomPos;
    private bool done;
    
    /* TO DO:
    * Spawn enemies (random enemy, position, and amount)
    * Spawn furniture and items
    * Detect items left in rooms (icons on minimap)
    */
    
    void Start()
    {
        roomPos = gameObject.transform.position;
        allRooms = GameObject.Find("/Manager").GetComponent<RoomLister>();
        if (isSpawn)
        {
            randomizer = Random.Range(4, 7); //Always has 3 or 4 doors
            directionNum = Random.Range(1, 4); //First room has no direction num, makes one up
            Invoke(nameof(Generate), 0.05f);
        }
        else
        {
            randomizer = Random.Range(0, 7);
            Invoke(nameof(Generate), Random.Range(0.05f, 0.95f)); //prevents rooms deleting each other at same time
        }
    }

    void Generate()
    {
        if (!roomGenerated && totalRooms.value < allRooms.array.Length)
        {
            if (directionNum == 1)
            {
                var newRoom = Instantiate(southRooms.array[randomizer], gameObject.transform.position, southRooms.array[randomizer].transform.rotation);
                newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 2)
            {
                var newRoom = Instantiate(westRooms.array[randomizer], gameObject.transform.position, westRooms.array[randomizer].transform.rotation);
                newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 3)
            {
                var newRoom = Instantiate(northRooms.array[randomizer], gameObject.transform.position, northRooms.array[randomizer].transform.rotation);
                newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 4)
            {
                var newRoom = Instantiate(eastRooms.array[randomizer], gameObject.transform.position, eastRooms.array[randomizer].transform.rotation);
                newRoom.transform.parent = gameObject.transform;
            }
            totalRooms.value++;
            roomGenerated = true;

            for (int i = 0; i < allRooms.array.Length; i++)
            {
                if (allRooms.array[i] == null)
                {
                    allRooms.array[i] = gameObject;
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room") && other.GetComponent<OLDMAPGEN>().roomGenerated)
        {
            print("Destroyed due to overlap with existing room!");
            transform.position = new Vector3(roomPos.x, -50, roomPos.z);
            Destroy(gameObject);
        }
    }
}