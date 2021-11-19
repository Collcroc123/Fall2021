using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleEffect : MonoBehaviour
{
    private string titleOne = "INFINIST", titleTwo = "¿ŋƒιπĩ$τ";
    private char[] chars, syms;
    private bool coro;
    private Text titleText;

    void Start()
    {
        titleText = GetComponent<Text>();
        titleText.text = titleOne;
    }
    
    void Update()
    {
        //if (!coro)
            //StartCoroutine(titleReplacer());
    }

    IEnumerator titleJumbler()
    {
        coro = true;
        int num = Random.Range(0, titleOne.Length);
        yield return new WaitForSeconds(0.1f);
        chars = titleOne.ToCharArray();
        syms = titleTwo.ToCharArray();
        chars[num] = syms[num];
        titleText.text = new string(chars);
        coro = false;
    }

    IEnumerator titleReplacer()
    {
        coro = true;
        float num = Random.Range(0.05f, 2f);
        float flicker = Random.Range(0.05f, 2f);
        yield return new WaitForSeconds(num);
        titleText.text = titleTwo;
        yield return new WaitForSeconds(flicker);
        titleText.text = titleOne;
        coro = false;
    }
}
