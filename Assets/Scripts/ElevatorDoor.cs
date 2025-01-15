using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Transform leftDoor; // Sol kapýnýn Transform bileþeni
    public Transform rightDoor; // Sað kapýnýn Transform bileþeni
    public Vector3 leftClosedLocalPosition; // Sol kapýnýn kapalý yerel pozisyonu
    public Vector3 rightClosedLocalPosition; // Sað kapýnýn kapalý yerel pozisyonu
    public Vector3 leftOpenLocalPosition; // Sol kapýnýn açýk yerel pozisyonu
    public Vector3 rightOpenLocalPosition; // Sað kapýnýn açýk yerel pozisyonu
    public float doorSpeed = 2f; // Kapýlarýn hareket hýzý
    public float doorCloseDelay = 2f; // Kapýlarýn kapanma gecikmesi

    private bool isClosing = false; // Kapýlarýn kapanýp kapanmadýðýný takip eder

    void Update()
    {
        if (isClosing)
        {
            // Kapýlarý kapalý yerel pozisyona doðru hareket ettir
            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftClosedLocalPosition, doorSpeed * Time.deltaTime);
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightClosedLocalPosition, doorSpeed * Time.deltaTime);

            // Kapýlar kapalý yerel pozisyona ulaþtýðýnda isClosing bayraðýný false yap
            if (leftDoor.localPosition == leftClosedLocalPosition && rightDoor.localPosition == rightClosedLocalPosition)
            {
                isClosing = false;
                Invoke("OpenDoors", doorCloseDelay); // Belirli bir süre sonra kapýlarý aç
            }
        }
    }

    public void CloseDoors()
    {
        isClosing = true; // Kapýlarý kapatmaya baþla
    }

    private void OpenDoors()
    {
        // Kapýlarý açýk yerel pozisyona doðru hareket ettir
        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftOpenLocalPosition, doorSpeed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightOpenLocalPosition, doorSpeed * Time.deltaTime);
    }
}