using UnityEngine;

[CreateAssetMenu]
public class FloatData : ScriptableObject
{
    public float value = 1f;

    public void ChangeValue(float number)
    {
        value += number;
    }
}