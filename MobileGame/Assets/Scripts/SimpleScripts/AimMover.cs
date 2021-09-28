using UnityEngine;
using UnityEngine.InputSystem;

public class AimMover : MonoBehaviour
{
    public bool aim, lookAtMove;
    private GameObject player, sprite, aimStick;
    private SpriteRenderer aimSprite;
    public BoolData controls;
    private Camera mainCamera;

    private void Start()
    {
        player = GameObject.Find("Player");
        sprite = GameObject.Find("Sprite");
        aimStick = GameObject.Find("LookArea");
        aimSprite = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        if (aim)
        {
            if (controls.mouse) //NOT WORKING PROPERLY
            {
                aimStick.SetActive(false);
                gameObject.transform.parent = null;
                //aim.transform.localPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, aim.transform.localPosition.y, Input.mousePosition.y));
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition); //(9.5f,0,5) full, (1.25f,0,3.75f) small
                gameObject.transform.localPosition = new Vector3(Input.mousePosition.x/125, gameObject.transform.localPosition.y, Input.mousePosition.y/125) - new Vector3(1.25f,0,3.75f);
            }
            else if (controls.gamepad)
            {
                if (controls.touch) { aimStick.SetActive(true); }
                else { aimStick.SetActive(false); }
                gameObject.transform.parent = player.gameObject.transform;
                if (Gamepad.current != null)
                {
                    gameObject.transform.localPosition = new Vector3(Gamepad.current.rightStick.x.ReadValue() * 6f, gameObject.transform.localPosition.y, Gamepad.current.rightStick.y.ReadValue() * 6f);
                }
            }
            
            if (gameObject.transform.localPosition.x > 1.5f || gameObject.transform.localPosition.x < -1.5f || gameObject.transform.localPosition.z > 1.5f || gameObject.transform.localPosition.z < -1.5f)
            {
                Vector3 relativePos = gameObject.transform.position - player.transform.position; relativePos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                sprite.transform.rotation = rotation;
                aimSprite.enabled = true;
            }
            else
            {
                sprite.transform.rotation = sprite.transform.rotation;
                aimSprite.enabled = false;
            }
        }
        else if (!aim)
        {
            if (lookAtMove)
            {
                if (gameObject.transform.localPosition.x > 0.1f || gameObject.transform.localPosition.x < -0.1f || gameObject.transform.localPosition.z > 0.1f || gameObject.transform.localPosition.z < -0.1f)
                {
                    Vector3 relativePos = gameObject.transform.position - player.transform.position; relativePos.y = 0;
                    Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    sprite.transform.rotation = rotation;
                }
            }
        }
    }
}