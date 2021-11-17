using UnityEngine;

[CreateAssetMenu]
public class BoolData : ScriptableObject
{
    public bool value, keyboard, mouse, touch, gamepad;

    public void ToggleBool(bool var) { var = !var; }
}