using UnityEngine;

public class AimMover : MonoBehaviour
{
    public GameObject stick;

    void Update()
    {
        gameObject.transform.localPosition = new Vector3(stick.transform.localPosition.x * 0.1f, 1.1f, stick.transform.localPosition.y * 0.1f);
    }
}