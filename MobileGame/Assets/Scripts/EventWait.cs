using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventWait : MonoBehaviour
{
    public Animator door, cam, uiFade;
    public AudioSource open;//, woosh;
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
        //woosh.Play();
        yield return new WaitForSeconds(num*3);
        door.SetBool("Open", false);
        SceneManager.LoadScene("Level"); 
    }
}