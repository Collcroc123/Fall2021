using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public int directionNum; //1234 = SWNE
    public ArrayData northRooms, eastRooms, southRooms, westRooms;
    public GameObject manager;
    private RoomLister allRooms;
    private bool roomGenerated;
    private int randomizer;
    public bool isSpawn = false, isEnd = false;
    public IntData totalRooms;

    void Start()
    {
        manager = GameObject.Find("/Manager");
        allRooms = manager.GetComponent<RoomLister>();
        if (isSpawn)
        {
            randomizer = Random.Range(1, 7);
            directionNum = Random.Range(1, 4);
            Invoke("Generate", 0.05f);
        }
        else
        {
            randomizer = Random.Range(0, 7);
            Invoke("Generate", Random.Range(0.05f, 1f));
        }
    }

    void Generate()
    {
        if (!roomGenerated && totalRooms.value < allRooms.array.Length)
        {
            //if (totalRooms.value == allRooms.array.Length) { isEnd = true; }
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
        if (other.CompareTag("Room") && other.GetComponent<MapGenerator>().roomGenerated)
        {
            print("Destroyed due to overlap with existing wall!");
            Destroy(gameObject);
        }
    }
}