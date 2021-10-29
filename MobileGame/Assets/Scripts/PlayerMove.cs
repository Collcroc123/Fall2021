using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller; //moves player
    private PlayerControls input; //gets player inputs
    public GameObject moveStick; //touchscreen stick to move
    public BoolData controls; //determines control scheme (REPLACE)
    public GunData gun; //current equipped gun
    public GameObject bulletSpawn; //location where bullet spawns
    private bool isShooting; //checks if player is shooting
    public float speed = 5f; //movement speed
    
    private void Start()
    { //sets up controls
        controller = GetComponent<CharacterController>();
        input = new PlayerControls();
        if (controls.keyboard)
            moveStick.SetActive(false);
        else if (controls.gamepad)
        {
            if (controls.touch) 
                moveStick.SetActive(true);
            else 
                moveStick.SetActive(false);
        }
        
        if (controls.touch && !controls.gamepad) 
            print("ERROR: GAMEPAD MUST BE ENABLED FOR TOUCH!!!");
        if (controls.mouse && controls.gamepad) 
            print("ERROR: YOU CANNOT HAVE BOTH MOUSE AND GAMEPAD ENABLED!!!");
    }

    public void EnableInput() { input.Enable(); }
    public void DisableInput() { input.Disable(); }

    void Update()
    { // Unity Docs
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
        
        if (controls.keyboard)
        { //checks for keyboard input
            Vector3 posi = new Vector3();
            if (Input.GetKey("w")) posi.z = 1;
            if (Input.GetKey("s")) posi.z = -1;
            if (Input.GetKey("a")) posi.x = -1;
            if (Input.GetKey("d")) posi.x = 1;
            if (!Input.anyKey) posi = Vector3.zero;
            Vector3 movePos = posi * speed;
            controller.Move(movePos * Time.deltaTime);
            if (Input.GetKey(KeyCode.Mouse0) && !isShooting) 
                StartCoroutine(Shoot());
        }
        
        else if (controls.gamepad && Gamepad.current != null)
        { //checks for controller/touch input
            Vector3 direction = new Vector3(Gamepad.current.leftStick.x.ReadValue(), 0f, Gamepad.current.leftStick.y.ReadValue());
            Vector3 aimDirection = new Vector3(Gamepad.current.rightStick.x.ReadValue(), 0f, Gamepad.current.rightStick.y.ReadValue());
            if (direction.magnitude >= 0.1f)
            {
                Vector3 moveVector = direction * (direction.magnitude * speed);
                controller.Move(moveVector * Time.deltaTime);
            }
            if (controls.touch && aimDirection.magnitude > 0.25f && !isShooting)
            {
                StartCoroutine(Shoot());
            }
            else if (!controls.touch && Gamepad.current.rightTrigger.ReadValue() > 0.1f && !isShooting)
            {
                StartCoroutine(Shoot());
            }
            
            
        }
    }

    IEnumerator Shoot()
    { //shoots bullet(s)
        isShooting = true;
        Bullet bullet = Instantiate(gun.bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation).GetComponent<Bullet>();
        bullet.bulletSpawn = bulletSpawn;
        yield return new WaitForSeconds(gun.fireRate);
        isShooting = false;
    }
}
/* if (Gamepad.current != null) print(Gamepad.current);
 * else if (Mouse.current != null && Keyboard.current != null) print(Mouse.current + " and " + Keyboard.current);
 * else if (Touchscreen.current != null) print(Touchscreen.current);
 */