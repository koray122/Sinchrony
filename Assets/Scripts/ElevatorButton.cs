using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ElevatorDoor elevatorDoor; // ElevatorDoor scriptine referans
    public float interactionDistance = 2f; // Etkileþim mesafesi
    private GameObject player; // Oyuncu referansý

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Oyuncu referansýný bul
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // E tuþuna basýldýðýnda kapýlarý kapat veya aç
                elevatorDoor.CloseDoors();
            }
        }
    }
}