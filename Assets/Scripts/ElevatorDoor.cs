using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Animator leftDoorAnimator; // Sol kapýnýn Animator bileþeni  
    public Animator rightDoorAnimator; // Sað kapýnýn Animator bileþeni  
    public float doorCloseDelay = 2f; // Kapýlarýn kapanma gecikmesi  

    private bool isClosing = false; // Kapýlarýn kapanýp kapanmadýðýný takip eder  
    public bool AreDoorsOpen { get; private set; } = false; // Kapýlarýn açýk mý olduðunu tutan deðiþken  

    void Start()
    {
        // Baþlangýçta kapýlarýn kapanýk olduðuna emin ol  
        leftDoorAnimator.SetBool("IsClosing", false);
        rightDoorAnimator.SetBool("IsClosing", false);
        leftDoorAnimator.SetBool("IsOpening", false);
        rightDoorAnimator.SetBool("IsOpening", false);
    }

    void Update()
    {
        if (isClosing)
        {
            // Kapýlar kapalý pozisyona ulaþtýðýnda isClosing bayraðýný false yap  
            if (leftDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftDoorClose") &&
                rightDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightDoorClose"))
            {
                isClosing = false;
                AreDoorsOpen = false; // Kapýlar kapalý  
            }
        }
        else
        {
            // Kapýlar açýldýysa durum güncelle  
            if (leftDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftDoorOpen") &&
                rightDoorAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightDoorOpen"))
            {
                AreDoorsOpen = true; // Kapýlar açýk  
            }
        }
    }

    public void CloseDoors()
    {
        if (!isClosing) // Zaten kapanýyorsa kontrol et  
        {
            isClosing = true; // Kapýlarý kapatmaya baþla  
            leftDoorAnimator.SetBool("IsClosing", true);
            rightDoorAnimator.SetBool("IsClosing", true);
            leftDoorAnimator.SetBool("IsOpening", false);
            rightDoorAnimator.SetBool("IsOpening", false);
        }
    }

    public void OpenDoors()
    {
        if (!isClosing && !AreDoorsOpen) // Halihazýrda kapanýyorsa ve kapýlar kapalýysa kontrol et  
        {
            leftDoorAnimator.SetBool("IsOpening", true);
            rightDoorAnimator.SetBool("IsOpening", true);
        }
    }
}