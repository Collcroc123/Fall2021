using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Random = UnityEngine.Random;

public class WallGenerator : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public ArrayData roomParts; //shortWall 0, longWall 1, shortDoor 2, longDoor 3
    public IntData totalRooms;
    public bool isShort;
    private Vector3 roomPos;
    private bool done;
    private bool isDoor, wallGenerated;
    private RoomLister allRooms;

    void Start()
    {
        allRooms = GameObject.Find("/Manager").GetComponent<RoomLister>();
        if (Random.Range(0f, 1f) >= 0.5f)
        {
            isDoor = true;
        }
        Invoke(nameof(Generate), Random.Range(0.05f, 0.95f)); //prevents walls deleting each other at same time
        //Invoke(nameof(Generate), 0.5f);
    }
    
    
    void Generate()
    {
        if (!wallGenerated && totalRooms.value < allRooms.array.Length)
        {
            if (isDoor)
            {
                if (isShort)
                {
                    var newWall = Instantiate(roomParts.array[2], gameObject.transform.position, gameObject.transform.rotation);
                    newWall.transform.parent = gameObject.transform;
                }
                else if (!isShort)
                {
                    var newWall = Instantiate(roomParts.array[3], gameObject.transform.position, gameObject.transform.rotation);
                    newWall.transform.parent = gameObject.transform;
                }
            }
            else if (!isDoor)
            {
                if (isShort)
                {
                    var newWall = Instantiate(roomParts.array[0], gameObject.transform.position, gameObject.transform.rotation);
                    newWall.transform.parent = gameObject.transform;
                }
                else if (!isShort)
                {
                    var newWall = Instantiate(roomParts.array[1], gameObject.transform.position, gameObject.transform.rotation);
                    newWall.transform.parent = gameObject.transform;
                }
            }
            wallGenerated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && other.GetComponent<WallGenerator>().wallGenerated)
        {
            print("Destroyed due to overlap with existing wall");
            Destroy(gameObject);
        }
    }
}