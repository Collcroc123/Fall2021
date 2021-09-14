using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 6f;
    private PlayerControls input;
    public GameObject sprite;
    private GameObject aim;
    private GameObject move;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        aim = GameObject.Find("Aim");
        move = GameObject.Find("MoveFacer");
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

        if (aim.transform.localPosition.x > 1f || aim.transform.localPosition.x < -1f || aim.transform.localPosition.z > 1f || aim.transform.localPosition.z < -1f)
        {
            sprite.transform.LookAt(new Vector3(aim.transform.position.x, 0, aim.transform.position.z));
        }
        else if (move.transform.localPosition.x > 0.1f || move.transform.localPosition.x < -0.1f || move.transform.localPosition.z > 0.1f || move.transform.localPosition.z < -0.1f)
        {
            sprite.transform.LookAt(new Vector3(move.transform.position.x, 0, move.transform.position.z));
        }
        else
        {
            sprite.transform.rotation = sprite.transform.rotation;
        }
    }
}