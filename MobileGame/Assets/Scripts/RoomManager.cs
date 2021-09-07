using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public HoleFiller door1, door2, door3, door4; //All doors in the room
    private RoomGenerator mapGen; //Auto sets spawn room as complete
    public bool roomComplete; //True if no enemies in room, controls door state
    private bool done; //If done=true, keeps door from ever opening or closing

    void Start()
    {
        roomComplete = true;
        mapGen = GetComponentInParent<RoomGenerator>();
        if (mapGen.isSpawn) { roomComplete = true; }
        Invoke(nameof(Open), 0.5f);
    }

    private void Update()
    {
        if (roomComplete && !done)
        {
            done = true;
            Invoke(nameof(Open), 0.5f);
        }
    }
    
    private void Open()
    {
        if (door1 != null) { door1.OpenDoor(); }
        if (door2 != null) { door2.OpenDoor(); }
        if (door3 != null) { door3.OpenDoor(); }
        if (door4 != null) { door4.OpenDoor(); }
    }

    private void Close()
    {
        if (door1 != null) { door1.CloseDoor(); }
        if (door2 != null) { door2.CloseDoor(); }
        if (door3 != null) { door3.CloseDoor(); }
        if (door4 != null) { door4.CloseDoor(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!roomComplete)
            {
                Close();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            roomComplete = true;
        }
    }
}
