using UnityEngine;
//using Random = UnityEngine.Random;

public class OLDMAPGEN : MonoBehaviour
{   //https://www.youtube.com/watch?v=qAf9axsyijY
    public ArrayData northRooms, eastRooms, southRooms, westRooms; //room prefabs
    public IntData totalRooms, maxRooms; //tracks rooms
    public GameObjectData roomObj; //tracks start & end rooms
    public GameObject emptyRoom; //test room
    public int directionNum; //determines facing direction
    [HideInInspector] public bool isSpawn, isEnd; //given to other scripts
    private bool roomGenerated; //marks room as complete
    private int randomizer; //picks random room prefab
    private Vector3 roomPos; //stores room position
    //private bool done; //

    void Start()
    {
        roomPos = gameObject.transform.position;
        if (isSpawn)
        {
            roomObj.start = gameObject;
            randomizer = Random.Range(4, 7); //Always has 3 or 4 doors
            //directionNum = Random.Range(1, 4); //First room has no direction num, makes one up
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
        if (!roomGenerated && totalRooms.value < maxRooms.value) 
        {
            if (directionNum == 0)
            {
                var newRoom = Instantiate(emptyRoom, gameObject.transform.position, emptyRoom.transform.rotation);
                newRoom.transform.parent = gameObject.transform;
            }
            else if (directionNum == 1)
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
            roomObj.end = gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room") && other.GetComponent<OLDMAPGEN>().roomGenerated)
        {
            if (roomObj.end == gameObject)
            {
                roomObj.end = other.gameObject;
            }
            print("Destroyed itself due to overlap with existing room!");
            transform.position = new Vector3(roomPos.x, -50, roomPos.z);
            Destroy(gameObject);
        }
    }
}