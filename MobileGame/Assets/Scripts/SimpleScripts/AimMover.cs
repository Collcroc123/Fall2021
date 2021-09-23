using UnityEngine;
using UnityEngine.InputSystem;

public class AimMover : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current != null)
        {
            gameObject.transform.localPosition = new Vector3(Gamepad.current.rightStick.x.ReadValue() * 6f, gameObject.transform.localPosition.y, Gamepad.current.rightStick.y.ReadValue() * 6f);
        }
    }
}