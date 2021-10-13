using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ArrayData array;
    public bool levelDone;
    public Animator genPanel;
    public PlayerMove movement;
    public IntData totalRooms;
    public StatsData stats;

    private void Start()
    {
        movement.enabled = false;
        totalRooms.value = 0;
        array.Clear();
        StartCoroutine(CheckDone());
    }
    
    IEnumerator CheckDone()
    {
        for (int i = 0; i < array.array.Length; i++)
        {
            if (array.array[i] == null)
            {
                yield return new WaitForSeconds(1f);
                if (array.array[i] == null)
                {
                    array.array[i-1].GetComponent<OLDMAPGEN>().isEnd = true;
                    levelDone = true;
                    print("Finished!");
                    genPanel.SetBool("LoadingDone", true);
                    movement.enabled = true;
                    yield break;
                }
            }
            else if (i == array.array.Length-1 && array.array[i] != null)
            {
                array.array[i].GetComponent<OLDMAPGEN>().isEnd = true;
                levelDone = true;
                print("Finished!");
                genPanel.SetBool("LoadingDone", true);
                movement.enabled = true;
                yield break;
            }
        }
    }
}