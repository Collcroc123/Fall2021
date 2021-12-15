using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator genPanel; // Loading screen
    public PlayerMove movement; // Script that controls player
    public IntData totalRooms, maxRooms, levelCount; // Keeps track of rooms
    public HealthData health;
    public GameObjectData lastRoom; // Stores final room generated
    public StatsData stats; // Tracks statistics
    public bool levelDone; // True if level is done generating
    public AudioSource music; // Plays  music
    public Text scoreTitle, scoreNum; // Score that appears on death screen
    public GameObject stairs; // Stairs object that spawn at end of each level
    public BoolData canShoot; // Manages if enemies and player can shoot
    public AudioClip[] musicArray; // Array of music to play randomly
    public MaterialData roomTextures;
    
    private void Start()
    { //freezes player, starts generation check
        //if (levelCount.value == 1)
        
        canShoot.value = false;
        music.clip = musicArray[Random.Range(0, musicArray.Length)];
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
                    // TO FIX MAP NOT STOPPING GEN: CREATE 2 TEMP ROOMS, SWAP BETWEEN EVERY OTHER ROOM, IF LAST ROOM IS DELETED, GET 2ND TEMP
                }
            }
        }
    }

    void FinishMap()
    { //marks end room, finishes generation
        roomTextures.Randomize();
        //print(roomTextures.slot);
        lastRoom.end.GetComponent<OLDMAPGEN>().isEnd = true;
        Vector3 loc = lastRoom.end.transform.position;
        stairs.transform.position = new Vector3(loc.x, loc.y + 1.125f, loc.z);
        levelDone = true;
        canShoot.value = true;
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