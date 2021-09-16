using UnityEngine;

public class AimMover : MonoBehaviour
{
    public GameObject stick;

    void FixedUpdate()
    {
        gameObject.transform.localPosition = new Vector3(stick.transform.localPosition.x * 0.1f, gameObject.transform.localPosition.y, stick.transform.localPosition.y * 0.1f);
    }
}