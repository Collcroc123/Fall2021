using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private PlayerControls input;
    public GameObject moveStick;
    public BoolData controls;
    public GunData gun;
    public GameObject bulletSpawn;
    private bool isShooting;
    public float speed = 7f;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        input = new PlayerControls();
    }

    private void OnEnable() { input.Enable(); }
    private void OnDisable() { input.Disable(); }

    void Update()
    {
        // Unity Docs
        InputSystem.onDeviceChange +=
            (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        Debug.Log("Device added: " + device);
                        break;
                    case InputDeviceChange.Removed:
                        Debug.Log("Device removed: " + device);
                        break;
                    case InputDeviceChange.ConfigurationChanged:
                        Debug.Log("Device configuration changed: " + device);
                        break;
                }
            };
        /*
        if (Gamepad.current != null)
        {
            print(Gamepad.current);
        }
        else if (Mouse.current != null && Keyboard.current != null)
        {
            print(Mouse.current + " and " + Keyboard.current);
        }
        else if (Touchscreen.current != null)
        {
            print(Touchscreen.current);
        }*/
        if (controls.keyboard)
        {
            moveStick.SetActive(false);
            Vector3 posi = new Vector3();
            if (Input.GetKey("w")) posi.z = 1;
            if (Input.GetKey("s")) posi.z = -1;
            if (Input.GetKey("a")) posi.x = -1;
            if (Input.GetKey("d")) posi.x = 1;
            if (!Input.anyKey) posi = Vector3.zero;
            controller.Move(posi * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.Mouse0) && !isShooting) StartCoroutine(Shoot());
        }
        else if (controls.gamepad && Gamepad.current != null)
        {
            Vector3 direction = new Vector3(Gamepad.current.leftStick.x.ReadValue(), 0f, Gamepad.current.leftStick.y.ReadValue());
            if (controls.touch) moveStick.SetActive(true);
            else
            {
                moveStick.SetActive(false);
                if (Gamepad.current.rightTrigger.ReadValue() > 0.1f && !isShooting) StartCoroutine(Shoot());
            }
            
            if (direction.magnitude >= 0.1f)
            {
                controller.Move(direction * (direction.magnitude*speed) * Time.deltaTime);
                if (controls.touch && !isShooting) StartCoroutine(Shoot());
            }
        }
        
        if (controls.touch && !controls.gamepad) print("ERROR: GAMEPAD MUST BE ENABLED FOR TOUCH!!!");
        if (controls.mouse && controls.gamepad) print("ERROR: YOU CANNOT HAVE BOTH MOUSE AND GAMEPAD ENABLED!!!");
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        print("SHOOTING!");
        Instantiate(gun.bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        yield return new WaitForSeconds(gun.fireRate);
        isShooting = false;
    }
}