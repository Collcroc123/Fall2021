using UnityEngine;

[CreateAssetMenu]
public class BoolData : ScriptableObject
{
    public bool keyboard;
    public bool touch;

    public void Toggle()
    {
        //if (value) { value = false; }
        //else { value = true; }
    }
}