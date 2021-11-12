using System.Collections;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    public float waitTime;
    
    void Start()
    {
        StartCoroutine(Delete(waitTime));
    }

    IEnumerator Delete(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
