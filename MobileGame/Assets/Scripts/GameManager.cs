using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator genPanel; //loading screen
    public PlayerMove movement; //script that controls player
    public IntData totalRooms, maxRooms; //keeps track of rooms
    public GameObjectData lastRoom; //stores final room generated
    public StatsData stats; //tracks statistics
    public bool levelDone; //true if level is done generating
    public AudioSource music;
    public ArrayData clips;
    public Text scoreTitle, scoreNum;
    
    private void Start()
    { //freezes player, starts generation check
        music.clip = clips.soundArray[Random.Range(0, clips.soundArray.Length)];
        music.Play();
        totalRooms.value = 0;
        //movement.DisableInput();
        movement.enabled = false;
        StartCoroutine(CheckDone());
    }
    
    IEnumerator CheckDone()
    { //checks if last room has been placed
        while (!levelDone)
        {
            if (totalRooms.value >= maxRooms.value)
            { //checks if current number of rooms exceed limit
                FinishMap();
            }
            else
            { //temp stores last room, waits, then checks if still last room
                GameObject tempRoom = lastRoom.end;
                yield return new WaitForSeconds(1f);
                if (lastRoom.end == tempRoom)
                {
                    FinishMap();
                }
            }
        }
    }

    void FinishMap()
    { //marks end room, finishes generation
        lastRoom.end.GetComponent<OLDMAPGEN>().isEnd = true;
        levelDone = true;
        genPanel.SetBool("LoadingDone", true);
        //movement.EnableInput();
        movement.enabled = true;
    }

    public void AddPoints(int points)
    {
        stats.currentScore += points;
        scoreNum.text = stats.currentScore.ToString();
        if (stats.currentScore > points)
        {
            stats.highScore = stats.currentScore;
            scoreTitle.text = "HIGH SCORE!";
        }
    }
}