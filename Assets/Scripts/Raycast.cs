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

    void Start()
    {
        if (hitText != null)
        {
            hitText.text = "";
        }
    }

    void Update()
    {
        if (mainCamera == null || renderTexture == null)
        {
            Debug.LogError("Ana kamera veya Render Texture atanmadý!");
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
            if (hitText != null)
            {
                hitText.text = hit.collider.gameObject.name;
            }

            // NoteController kontrolü
            NoteController noteController = hit.collider.GetComponent<NoteController>();
            if (noteController != null)
            {
                // Eðer bir NoteController'a raycast ediliyorsa
                if (Input.GetKeyDown(KeyCode.T))
                {
                    // Notu aç veya kapat
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

            // Eðer ray'in çarptýðý nesne "Silinebilir" etikete sahipse
            if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Silinebilir"))
            {
                // Çarpýlan nesneyi sahneden yok et
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            if (hitText != null)
            {
                hitText.text = "";
            }

            // Eðer raycast baþka bir yere gidiyorsa notu kapat
            if (currentNoteController != null)
            {
                currentNoteController.HideNote();
                currentNoteController = null;
            }
        }
    }
}
