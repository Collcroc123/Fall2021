using UnityEngine;
using Cinemachine;

public class RoomManager : MonoBehaviour
{
    public HoleFiller door1, door2, door3, door4; //All doors in the room
    public bool roomComplete; //True if no enemies in room, controls door state
    private MapGenerator mapGen; //Auto sets spawn room as complete
    private CinemachineSmoothPath track; //Set points along track for cam
    private Vector3 pos; //gameObject's coordinates
    private bool done; //If done=true, keeps door from ever opening or closing

    void Start()
    {
        pos = gameObject.transform.position;
        mapGen = GetComponentInParent<MapGenerator>();
        track = GameObject.Find("/DollyTrack").GetComponent<CinemachineSmoothPath>();
        track.m_Waypoints = new CinemachineSmoothPath.Waypoint[2];
        track.m_Waypoints[0].position = Vector3.zero;
        track.m_Waypoints[1].position = Vector3.zero;
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
            track.m_Waypoints[0].position = track.m_Waypoints[1].position; //FIX ISSUES!!!
            track.m_Waypoints[1].position = new Vector3(pos.x, 0, pos.z);
        }
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
}
