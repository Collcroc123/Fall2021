using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventWait : MonoBehaviour
{
    public Animator door, cam, uiFade;
    public void WaitFor(float time)
    {
        StartCoroutine(Wait(time));
    }
    
    IEnumerator Wait(float num)
    {
        yield return new WaitForSeconds(num/2);
        uiFade.SetTrigger("Fade");
        door.SetBool("Open", true);
        yield return new WaitForSeconds(num);
        cam.SetTrigger("Play");
        yield return new WaitForSeconds(num*2);
        door.SetBool("Open", false);
        SceneManager.LoadScene("Level"); 
    }
}