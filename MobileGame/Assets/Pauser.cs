using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    //public GameObject panel;
    public Animator fade;

    public void PauseGame()
    {
        Time.timeScale = 0;
        //panel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //panel.SetActive(false);
    }

    public void Fade()
    {
        StartCoroutine(waitFade());
    }

    IEnumerator waitFade()
    {
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Menu"); 
    }
}
