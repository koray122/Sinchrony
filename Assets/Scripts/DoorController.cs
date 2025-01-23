using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public bool isOpen = false;

    public void OpenDoor()
    {
        isOpen = true;
        doorAnimator.SetTrigger("Open");
    }

    public void CloseDoor()
    {
        isOpen = false;
        doorAnimator.SetTrigger("Close");
    }
}
