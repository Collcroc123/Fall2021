using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 6f;
    private PlayerControls input;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Update()
    {
        Vector2 stick = input.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(stick.x, 0f, stick.y).normalized;
        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}