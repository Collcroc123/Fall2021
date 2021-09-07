using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public IntData totalRooms;
    public bool isSpawn, isEnd;
    private RoomLister allRooms;
    private bool roomGenerated;
    private int randomizer;
    private bool done;
    public GameObject floor;
    
    /* TO DO:
    * Spawn enemies (random enemy, position, and amount)
    * Spawn furniture and items
    * Detect items left in rooms (icons on minimap)
    */
    
    void Start()
    {
        allRooms = GameObject.Find("/Manager").GetComponent<RoomLister>();
        //Invoke(nameof(Generate), Random.Range(0.05f, 0.95f)); //prevents rooms deleting each other at same time
        Invoke(nameof(Generate), 1f);
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
        if (other.CompareTag("Room") && other.GetComponent<RoomGenerator>().roomGenerated)
        {
            print("Destroyed due to overlap with existing room!");
            Destroy(gameObject);
        }
    }
}