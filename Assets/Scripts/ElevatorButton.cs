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
        // Oyuncu "Player" tag'ine sahip bir nesne ise etkileþim mesafesini kontrol et  
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
        {
            // "E" tuþuna basýldýðýnda kapýlarý aç/kapat  
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Eðer "InsideButton" tag'ine sahip bir nesneye týklanýyorsa kapýlarý kapat  
                if (gameObject.CompareTag("InsideButton"))
                {
                    // Kapýlarý kapat  
                    elevatorDoor.CloseDoors();
                }
                else
                {
                    // Diðer nesnelere týklanýyorsa kapýlarý aç  
                    if (elevatorDoor.AreDoorsOpen)
                    {
                        elevatorDoor.CloseDoors();
                    }
                    else
                    {
                        elevatorDoor.OpenDoors();
                    }
                }
            }
        }
    }
}