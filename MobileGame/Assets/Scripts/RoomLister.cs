using System.Collections;
using UnityEngine;

public class RoomLister : MonoBehaviour
{
    public GameObject[] array;
    public bool levelDone;
    public Animator genPanel;
    public PlayerMove movement;

    private void Start()
    {
        movement.enabled = false;
        StartCoroutine(CheckDone());
    }
    
    IEnumerator CheckDone()
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                yield return new WaitForSeconds(1f);
                if (array[i] == null)
                {
                    array[i-1].GetComponent<MapGenerator>().isEnd = true;
                    levelDone = true;
                    print("Finished!");
                    genPanel.SetBool("LoadingDone", true);
                    movement.enabled = true;
                    yield break;
                }
            }
            else if (i == array.Length-1 && array[i] != null)
            {
                array[i].GetComponent<MapGenerator>().isEnd = true;
                levelDone = true;
                print("Finished!");
                genPanel.SetBool("LoadingDone", true);
                movement.enabled = true;
                yield break;
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