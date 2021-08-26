using System;
using UnityEngine;

public class RoomLister : MonoBehaviour
{
    public GameObject[] array;
    public bool levelDone = false;

    private void Start()
    {
        Invoke("Ender", 10f);
    }

    void Ender()
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                array[i-1].GetComponent<MapGenerator>().isEnd = true;
                levelDone = true;
                print("Finished!");
                return;
            }
            else if (i == array.Length-1 && array[i] != null)
            {
                array[i].GetComponent<MapGenerator>().isEnd = true;
                levelDone = true;
                print("Finished!");
                return;
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = null;
        }
    }
}