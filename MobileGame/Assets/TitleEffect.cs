using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleEffect : MonoBehaviour
{
    private string title, titleOne = "INFINIST", titleTwo = "¿ŋƒιπĩ$τ";
    private bool coro;
    private Text titleText;

    void Start()
    {
        titleText = GetComponent<Text>();
    }
    
    void Update()
    {
        if (!coro)
            StartCoroutine(titleJumbler());
    }

    IEnumerator titleJumbler()
    {
        coro = true;
        yield return new WaitForSeconds(0.3f);
        int chr = Random.Range(0, titleOne.Length);
        title = titleOne.Replace(titleOne[chr], titleTwo[chr]);
        titleText.text = title;
        coro = false;
    }
}
