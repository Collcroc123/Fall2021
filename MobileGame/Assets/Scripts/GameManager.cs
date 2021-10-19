using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator genPanel;
    public PlayerMove movement;
    public IntData totalRooms, maxRooms;
    public GameObjectData lastRoom;
    public GameObject tempRoom;
    public StatsData stats;
    public bool levelDone;
    
    private void Start()
    {
        movement.enabled = false;
        totalRooms.value = 0;
        StartCoroutine(CheckDone());
    }
    
    IEnumerator CheckDone()
    {
        while (!levelDone)
        {
            if (totalRooms == maxRooms)
            {
                FinishMap();
            }
            else
            {
                tempRoom = lastRoom.end;
                yield return new WaitForSeconds(1f);
                if (lastRoom.end == tempRoom)
                {
                    FinishMap();
                }
            }
        }
    }

    void FinishMap()
    {
        lastRoom.end.GetComponent<OLDMAPGEN>().isEnd = true;
        levelDone = true;
        print("Finished!");
        genPanel.SetBool("LoadingDone", true);
        movement.enabled = true;
    }
}