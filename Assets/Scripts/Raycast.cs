using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshPro referansý
    public Camera mainCamera; // Ana kamera referansý
    public RenderTexture renderTexture; // Kullanýlan Render Texture
    public float rayLength = 100f; // Ray'in maksimum uzunluðu
    public Color rayColor = Color.red; // Ray'in rengi

    private NoteController currentNoteController; // Aktif NoteController referansý
    private Inventory inventory; // Envanter referansý

    void Start()
    {
        if (hitText != null)
        {
            hitText.text = ""; // Baþlangýçta hitText'in boþ olduðundan emin olun
        }

        // Inventory bileþenini bul
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory bileþeni bulunamadý!"); // Inventory bileþeni bulunamazsa hata mesajý ver
        }
    }

    void Update()
    {
        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Ana kamera veya Render Texture atanmadý!"); // Ana kamera veya Render Texture atanmadýysa hata mesajý ver
            return;
        }

        // Render Texture'un boyutlarýna göre ekran ortasýný hesapla
        Vector3 renderTextureCenter = new Vector3(renderTexture.width / 2, renderTexture.height / 2, 0);

        // Render Texture boyutlarýna göre bir ray oluþtur
        Ray ray = mainCamera.ScreenPointToRay(renderTextureCenter);
        RaycastHit hit;

        // Debug.DrawRay ile ray'i çiz
        Debug.DrawRay(ray.origin, ray.direction * rayLength, rayColor);

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            GameObject hitObject = hit.collider.gameObject; // Ray'in çarptýðý objeyi al

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
                    hitText.text = "Kapýyý aç/kapat (F)";
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