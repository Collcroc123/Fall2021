using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public GameManager manager;
    private CharacterController controller; // Moves Player
    private PlayerControls input; // Gets Player Inputs
    private SpriteRenderer aimSprite; // Aim Icon Sprite
    private bool isController, isTouchscreen, isKeyboard; // What Control System in Use
    private Vector3 movePos = Vector3.zero, aimPos = Vector3.zero; // Player and Aim Positions
    [HideInInspector] public GunManager gunMan; // Manages Shooting
    public GameObject aimStick, moveStick, aimIcon, moveIcon, playerSprite; // Touchscreen Sticks to Move and Aim
    public PlayerInput controls; // Gets Input
    public float speed = 5f; // Movement Speed
    public Camera mainCamera; // Camera
    
    
    private void Start()
    { //sets up controls
        controller = GetComponent<CharacterController>();
        gunMan = GetComponentInChildren<GunManager>();
        input = new PlayerControls();
        aimSprite = aimIcon.GetComponent<SpriteRenderer>();
        ControlsChange();
    }

    public void EnableInput() { input.Enable(); }
    public void DisableInput() { input.Disable(); }

    void Update()
    {
        controller.Move(movePos * Time.deltaTime);
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        //print(context);
        Vector2 moveDir = context.ReadValue<Vector2>();
        if (moveDir.magnitude >= 0.1f)
        {
            movePos.x = moveDir.x * (moveDir.magnitude * speed);
            movePos.z = moveDir.y * (moveDir.magnitude * speed);
        }
        else { movePos = Vector3.zero; }

        if (isController || isTouchscreen)
        {
            Vector3 relativePos = moveIcon.transform.position - gameObject.transform.position; relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            playerSprite.transform.rotation = rotation;
        }
    }
    
    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 aimDir = context.ReadValue<Vector2>();
        if (isKeyboard)
        {
            aimIcon.transform.parent = mainCamera.transform;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                aimPos = raycastHit.point;
                aimPos.y = 1;
                aimIcon.transform.position = aimPos;
                aimSprite.enabled = true;
                Vector3 relativePos = aimIcon.transform.position - gameObject.transform.position; relativePos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                playerSprite.transform.rotation = rotation;
            }
        }
        else if (isController || isTouchscreen)
        {
             aimIcon.transform.parent = gameObject.transform;
             aimIcon.transform.localPosition = new Vector3(aimDir.x * 6f, gameObject.transform.localPosition.y, aimDir.y * 6f);
             if (aimDir.magnitude > 0.25f)
             {
                 aimSprite.enabled = true;
                 Vector3 relativePos = aimIcon.transform.position - gameObject.transform.position; relativePos.y = 0;
                 Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                 playerSprite.transform.rotation = rotation;
                 if (isTouchscreen) { Shoot(); }
             }
             else
             {
                 aimPos = Vector3.zero;
                 playerSprite.transform.rotation = playerSprite.transform.rotation;
                 aimSprite.enabled = false;
             }
        }
    }

    public void Shoot()
    {
        if (!gunMan.isShooting)
        {
            gunMan.Shoot();
        }
    }
    
    public void ControlsChange()
    {
        moveStick.SetActive(false);
        aimStick.SetActive(true);
        if (controls.currentControlScheme == "Touchscreen")
        {
            isTouchscreen = true;
            moveStick.SetActive(true);
            aimStick.SetActive(true);
        }
        else if (controls.currentControlScheme == "Controller")
        {
            isController = true;
            moveStick.SetActive(false);
            aimStick.SetActive(false);
        }
        else if (controls.currentControlScheme == "Keyboard")
        {
            isKeyboard = true;
            moveStick.SetActive(false);
            aimStick.SetActive(false);
        }
        print("CONTROLS CHANGED TO " + controls.currentControlScheme);
    }
}