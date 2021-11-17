using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Animator roomFade; //black that covers outside rooms
    public DoorAnim[] doors; //all doors in the room
    private OLDMAPGEN mapGen; //sets spawn room as complete
    public bool roomComplete, playerEntered; //no enemies in room, opens doors
    private bool done; //keeps door from ever opening or closing
    public int enemyNum;
    public GameObject enemyPrefab;
    public GameObject cratePrefab;
    public IntData roomsSinceLastCrate;
    private AudioSource doorSource;
    public AudioClip open, close;
    public SpriteRenderer mapBG;
    public Color mapColor;
    //public Light lightOne, lightTwo;

    void Start()
    {
        //lightOne = GetComponentInChildren<Light>();
        //lightTwo = GetComponentInChildren<Light>();
        //roomComplete = true; //TEMP TRUE WITHOUT ENEMIES
        doorSource = GetComponent<AudioSource>();
        mapGen = GetComponentInParent<OLDMAPGEN>();
        if (mapGen.isSpawn) { roomComplete = true; }
        else
        {
            enemyNum = Random.Range(1, 2);
            for (int i = 0; i < enemyNum; i++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                newEnemy.transform.SetParent(gameObject.transform);
                newEnemy.transform.localPosition = new Vector3(Random.Range(-5f, 5f), 0.5f, Random.Range(-2.5f, 2.5f));
            }
        }
        Invoke(nameof(Open), 0.5f);
    }

    private void Update()
    {
        if (enemyNum <= 0)
        {
            roomComplete = true;
        }
        if (roomComplete && !done)
        {
            done = true;
            if (roomsSinceLastCrate.value >= 1) //3 and 3
            {
                if (Random.Range(0f, 5f) >= 0f)
                {
                    Vector3 pos = gameObject.transform.position;
                    Instantiate(cratePrefab, new Vector3(pos.x, pos.y+3, pos.z), Quaternion.identity);
                    roomsSinceLastCrate.value = -1;
                }
            }
            roomsSinceLastCrate.value++;
            Invoke(nameof(Open), 0.5f);
        }
    }
    
    private void Open()
    {
        foreach (var door in doors)
            door.OpenDoor();
        doorSource.clip = open;
        doorSource.Play();
    }

    private void Close()
    {
        foreach (var door in doors)
            door.CloseDoor();
        doorSource.clip = close;
        doorSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //lightOne.enabled = true;
            //lightTwo.enabled = true;
            StartCoroutine(Wait());
            playerEntered = true;
            roomFade.SetBool("Enter", true);
            mapBG.color = mapColor;
            if (!roomComplete)
            {
                doorSource.mute = false;
                Close();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //lightOne.enabled = false;
            //lightTwo.enabled = false;
            playerEntered = false;
            roomFade.SetBool("Enter", false);
        }
        else if (other.CompareTag("Enemy"))
            enemyNum--;
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}