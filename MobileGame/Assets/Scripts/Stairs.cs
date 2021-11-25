using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour
{
    public GameManager manager;
    public Animator fade;
    public BoolData canShoot;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.levelCount.value++;
            manager.stats.levelsCompleted++;
            manager.stats.currentScore += 1000;
            StartCoroutine(waitFade());
        }
    }
    
    IEnumerator waitFade()
    {
        canShoot.value = false;
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Level");
    }
}
