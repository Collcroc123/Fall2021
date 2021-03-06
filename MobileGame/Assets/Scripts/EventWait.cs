using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventWait : MonoBehaviour
{
    public AudioSource open;
    public Animator door, cam, uiFade, fadeOut;
    
    public void WaitFor(float time)
    {
        StartCoroutine(Wait(time));
    }
    
    IEnumerator Wait(float num)
    {
        yield return new WaitForSeconds(num/2);
        uiFade.SetTrigger("Fade");
        door.SetBool("Open", true);
        open.Play();
        yield return new WaitForSeconds(num);
        cam.SetTrigger("Play");
        yield return new WaitForSeconds(num*3);
        door.SetBool("Open", false);
        SceneManager.LoadScene("Level"); 
    }
    
    public void SceneChange(string load)
    {
        StartCoroutine(Scene(load));
    }
    
    IEnumerator Scene(string scene)
    {
        fadeOut.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene); 
    }
}