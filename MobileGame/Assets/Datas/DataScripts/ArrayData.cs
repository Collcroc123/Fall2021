using UnityEngine;

[CreateAssetMenu]
public class ArrayData : ScriptableObject
{
    public GameObject[] array;
    public IntData lastNum;

    public void Clear() 
    { //clears array
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = null;
        }
    }

    public void GetLast()
    { //Gets last slot in array
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                lastNum.value = i - 1;
                return;
            }
            else if (i == array.Length-1)
            {
                lastNum.value = i;
                return;
            }
        }
    }
}