using UnityEngine;

public class CamMover : MonoBehaviour
{
    public Transform cam;
    private float speed = 0.075f;
    private Vector3 pos;

    void Update()
    {
        cam.position = Vector3.Lerp(cam.position, pos, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoomManager>())
        {
            pos = other.transform.position; pos.y = 10;
        }
    }
}