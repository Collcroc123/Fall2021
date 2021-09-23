using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public IntData totalRooms;
    public bool isSpawn, isEnd;
    public ArrayData allRooms;
    private bool roomGenerated;
    private int randomizer;
    private bool done;
    public GameObject floor;

    void Start()
    {
        Invoke(nameof(Generate), Random.Range(0.05f, 0.95f)); //prevents rooms deleting each other at same time
    }

    void Generate()
    {
        if (!roomGenerated && totalRooms.value < allRooms.array.Length)
        {
            var newWall = Instantiate(floor, gameObject.transform.position, gameObject.transform.rotation);
            newWall.transform.parent = gameObject.transform;
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
        if (other.CompareTag("Room") && other.GetComponent<RoomGenerator>().roomGenerated) //other.CompareTag(null) == false && 
        {
            print("Destroyed due to overlap with existing room!");
            Destroy(gameObject);
        }
    }
}