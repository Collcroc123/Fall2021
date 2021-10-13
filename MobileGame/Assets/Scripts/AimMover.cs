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
            if (controls.mouse)
            {//https://stackoverflow.com/questions/46998241/getting-mouse-position-in-unity
                gameObject.transform.parent = mainCamera.transform;
                aimStick.SetActive(false);
                Ray castPoint = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                    gameObject.transform.position = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.z); // * 1.12f
            }
            else if (controls.gamepad)
            {
                if (controls.touch) 
                    aimStick.SetActive(true);
                else 
                    aimStick.SetActive(false);
                gameObject.transform.parent = player.gameObject.transform;
                if (Gamepad.current != null) 
                    gameObject.transform.localPosition = new Vector3(Gamepad.current.rightStick.x.ReadValue() * 6f, gameObject.transform.localPosition.y, Gamepad.current.rightStick.y.ReadValue() * 6f);
                
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