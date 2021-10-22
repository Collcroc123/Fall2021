using Cinemachine;
using UnityEngine;
using System.Collections;

public class CamMover : MonoBehaviour
{
    public Transform cam;
    public float speed = 0.08f;
    private Vector3 pos;
    private CinemachineVirtualCamera cmvCam;

    private void Start()
    {
        cmvCam = cam.GetComponent<CinemachineVirtualCamera>();
    }

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

    public void Shake(float inten, float dur)
    {
        StartCoroutine(ShakeTime(inten, dur));
    }

    IEnumerator ShakeTime(float intensity, float duration)
    {
        cmvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        cmvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}