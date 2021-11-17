using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    private Animator doorAnim; // Opens and closes doors
    
    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        doorAnim.SetBool("Open", true);
    }
    public void CloseDoor()
    {
        doorAnim.SetBool("Open", false);
    }
}
