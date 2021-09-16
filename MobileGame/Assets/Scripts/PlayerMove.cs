using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private GameObject aim, move, aimStick, moveStick, sprite;
    private SpriteRenderer aimSprite;
    private Camera mainCamera;
    private PlayerControls input;
    public float speed = 7f;
    public Toggle kbmToggle;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        aim = GameObject.Find("Aim");
        move = GameObject.Find("Move");
        aimStick = GameObject.Find("LookArea");
        moveStick = GameObject.Find("MoveArea");
        sprite = GameObject.Find("Sprite");
        aimSprite = aim.GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        input = new PlayerControls();
    }

    private void OnEnable() { input.Enable(); }
    private void OnDisable() { input.Disable(); }

    void FixedUpdate()
    {
        if (kbmToggle.isOn)
        {
            aimStick.SetActive(false);
            moveStick.SetActive(false);
            Vector3 posi = new Vector3();
            if (Input.GetKey("w")) { posi.z = 1; }
            if (Input.GetKey("s")) { posi.z = -1; }
            if (Input.GetKey("a")) { posi.x = -1; }
            if (Input.GetKey("d")) { posi.x = 1; }
            if (!Input.anyKey) { posi = Vector3.zero; }
            controller.Move(posi * speed * Time.deltaTime);
            move.transform.localPosition = posi;
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            aim.transform.localPosition = new Vector3(Input.mousePosition.x, aim.transform.localPosition.y, Input.mousePosition.y);
        }
        else if (!kbmToggle.isOn)
        {
            aimStick.SetActive(true);
            moveStick.SetActive(true);
            Vector2 stick = input.Player.Move.ReadValue<Vector2>();
            Vector3 direction = new Vector3(stick.x, 0f, stick.y);
            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * (direction.magnitude*speed) * Time.deltaTime);
            }
        }
        
        if (aim.transform.localPosition.x > 0.5f || aim.transform.localPosition.x < -0.5f || aim.transform.localPosition.z > 0.5f || aim.transform.localPosition.z < -0.5f)
        {
            Vector3 relativePos = aim.transform.position - transform.position; relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            sprite.transform.rotation = rotation;
            aimSprite.enabled = true;
        }
        else if (move.transform.localPosition.x > 0.1f || move.transform.localPosition.x < -0.1f || move.transform.localPosition.z > 0.1f || move.transform.localPosition.z < -0.1f)
        {
            Vector3 relativePos = move.transform.position - transform.position; relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            sprite.transform.rotation = rotation;
        }
        else
        {
            sprite.transform.rotation = sprite.transform.rotation;
            aimSprite.enabled = false;
        }
    }
}