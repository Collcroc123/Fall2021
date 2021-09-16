using UnityEngine;

[CreateAssetMenu]
public class ArrayData : ScriptableObject
{
    public GameObject[] array;

    public void Clear()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = null;
        }
    }
}