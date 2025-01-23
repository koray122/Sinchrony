using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshPro referans�
    public Camera mainCamera; // Ana kamera referans�
    public RenderTexture renderTexture; // Kullan�lan Render Texture
    public float rayLength = 100f; // Ray'in maksimum uzunlu�u
    public Color rayColor = Color.red; // Ray'in rengi

    private NoteController currentNoteController; // Aktif NoteController referans�
    private Inventory inventory; // Envanter referans�

    void Start()
    {
        if (hitText != null)
        {
            hitText.text = ""; // Ba�lang��ta hitText'in bo� oldu�undan emin olun
        }

        // Inventory bile�enini bul
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory bile�eni bulunamad�!"); // Inventory bile�eni bulunamazsa hata mesaj� ver
        }
    }

    void Update()
    {
        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Ana kamera veya Render Texture atanmad�!"); // Ana kamera veya Render Texture atanmad�ysa hata mesaj� ver
            return;
        }

        // Render Texture'un boyutlar�na g�re ekran ortas�n� hesapla
        Vector3 renderTextureCenter = new Vector3(renderTexture.width / 2, renderTexture.height / 2, 0);

        // Render Texture boyutlar�na g�re bir ray olu�tur
        Ray ray = mainCamera.ScreenPointToRay(renderTextureCenter);
        RaycastHit hit;

        // Debug.DrawRay ile ray'i �iz
        Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            GameObject hitObject = hit.collider.gameObject; // Ray'in �arpt��� objeyi al

            if (hitText != null)
            {
                if (hitObject.CompareTag("Silinebilir"))
                {
                    hitText.text = $"Envantere ekle: {hitObject.name}";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        inventory.AddItem(hitObject);
                    }
                }
                else if (hitObject.CompareTag("Tasinabilir"))
                {
                    hitText.text = $"Al: {hitObject.name}";
                }
                else if (hitObject.CompareTag("Okunabilir"))
                {
                    hitText.text = $"Oku: {hitObject.name}";
                    NoteController noteController = hitObject.GetComponent<NoteController>();
                    if (noteController != null && Input.GetKeyDown(KeyCode.T))
                    {
                        if (currentNoteController == noteController)
                        {
                            currentNoteController.HideNote();
                            currentNoteController = null;
                        }
                        else
                        {
                            if (currentNoteController != null)
                            {
                                currentNoteController.HideNote();
                            }
                            currentNoteController = noteController;
                            currentNoteController.ShowNote();
                        }
                    }
                }
                else if (hitObject.CompareTag("Door"))
                {
                    hitText.text = "Kap�y� a�/kapat (F)";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        DoorController doorController = hitObject.GetComponent<DoorController>();
                        if (doorController != null)
                        {
                            if (doorController.isOpen)
                            {
                                doorController.CloseDoor();
                            }
                            else
                            {
                                doorController.OpenDoor();
                            }
                        }
                    }
                }
                else
                {
                    hitText.text = hitObject.name;
                }
            }
        }
        else
        {
            if (hitText != null)
            {
                hitText.text = "";
            }

            if (currentNoteController != null)
            {
                currentNoteController.HideNote();
                currentNoteController = null;
            }
        }
    }
}