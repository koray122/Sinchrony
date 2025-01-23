using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public ElevatorDoor elevatorDoor; // ElevatorDoor scriptine referans  
    public float interactionDistance = 2f; // Etkile�im mesafesi  
    private GameObject player; // Oyuncu referans�  

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Oyuncu referans�n� bul  
    }

    void Update()
    {
        // Oyuncu "Player" tag'ine sahip bir nesne ise etkile�im mesafesini kontrol et  
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= interactionDistance)
        {
            // "E" tu�una bas�ld���nda kap�lar� a�/kapat  
            if (Input.GetKeyDown(KeyCode.E))
            {
                // E�er "InsideButton" tag'ine sahip bir nesneye t�klan�yorsa kap�lar� kapat  
                if (gameObject.CompareTag("InsideButton"))
                {
                    // Kap�lar� kapat  
                    elevatorDoor.CloseDoors();
                }
                else
                {
                    // Di�er nesnelere t�klan�yorsa kap�lar� a�  
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