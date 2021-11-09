using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator genPanel; //loading screen
    public PlayerMove movement; //script that controls player
    public IntData totalRooms, maxRooms; //keeps track of rooms
    public GameObjectData lastRoom; //stores final room generated
    public GameObject tempRoom; //temporarily stores last room
    public StatsData stats; //tracks statistics
    public bool levelDone; //true if level is done generating
    public AudioSource music;
    public ArrayData clips;
    public Text scoreTitle, scoreNum;
    
    private void Start()
    { //freezes player, starts generation check
        //movement.DisableInput();
        music.clip = clips.soundArray[Random.Range(0, clips.soundArray.Length)];
        music.Play();
        movement.enabled = false;
        totalRooms.value = 0;
        StartCoroutine(CheckDone());
    }
    
    IEnumerator CheckDone()
    { //checks if last room has been placed
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
    { //sets end room, finishes generation
        lastRoom.end.GetComponent<OLDMAPGEN>().isEnd = true;
        levelDone = true;
        print("Map Finished!");
        genPanel.SetBool("LoadingDone", true);
        //movement.EnableInput();
        movement.enabled = true;
    }
}