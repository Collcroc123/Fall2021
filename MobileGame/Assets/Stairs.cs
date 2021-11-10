using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public GameManager manager;
    public Animator fade;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.levelCount.value++;
            manager.stats.levelsCompleted++;
            StartCoroutine(waitFade());
        }
    }
    
    IEnumerator waitFade()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level"); 
    }
}
