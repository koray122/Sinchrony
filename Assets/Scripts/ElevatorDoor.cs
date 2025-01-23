using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Animator leftDoorAnimator; // Sol kap�n�n Animator bile�eni  
    public Animator rightDoorAnimator; // Sa� kap�n�n Animator bile�eni  
    public float doorCloseDelay = 2f; // Kap�lar�n kapanma gecikmesi  

    private bool isClosing = false; // Kap�lar�n kapan�p kapanmad���n� takip eder  
    public bool AreDoorsOpen { get; private set; } = false; // Kap�lar�n a��k m� oldu�unu tutan de�i�ken  

    void Start()
    {
        // Ba�lang��ta kap�lar�n kapan�k oldu�una emin ol  
        leftDoorAnimator.SetBool("IsClosing", false);
        rightDoorAnimator.SetBool("IsClosing", false);
        leftDoorAnimator.SetBool("IsOpening", false);
        rightDoorAnimator.SetBool("IsOpening", false);
    }

    void Update()
    {
        if (isClosing)
        {
            // Kap�lar kapal� pozisyona ula�t���nda isClosing bayra��n� false yap  
            if (leftDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftDoorClose") &&
                rightDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightDoorClose"))
            {
                isClosing = false;
                AreDoorsOpen = false; // Kap�lar kapal�  
            }
        }
        else
        {
            // Kap�lar a��ld�ysa durum g�ncelle  
            if (leftDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftDoorOpen") &&
                rightDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightDoorOpen"))
            {
                AreDoorsOpen = true; // Kap�lar a��k  
            }
        }
    }

    public void CloseDoors()
    {
        if (!isClosing) // Zaten kapan�yorsa kontrol et  
        {
            isClosing = true; // Kap�lar� kapatmaya ba�la  
            leftDoorAnimator.SetBool("IsClosing", true);
            rightDoorAnimator.SetBool("IsClosing", true);
            leftDoorAnimator.SetBool("IsOpening", false);
            rightDoorAnimator.SetBool("IsOpening", false);
        }
    }

    public void OpenDoors()
    {
        if (!isClosing && !AreDoorsOpen) // Halihaz�rda kapan�yorsa ve kap�lar kapal�ysa kontrol et  
        {
            leftDoorAnimator.SetBool("IsOpening", true);
            rightDoorAnimator.SetBool("IsOpening", true);
        }
    }
}