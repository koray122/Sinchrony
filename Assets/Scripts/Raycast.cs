using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshPro referansý
    public Camera mainCamera; // Ana kamera referansý
    public RenderTexture renderTexture; // Kullanýlan Render Texture
    public float rayLength = 100f; // Ray'in maksimum uzunluðu
    public Color rayColor = Color.red; // Ray'in rengi

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
                hitText.text = "Çarpýlan obje: " + hit.collider.gameObject.name;
            }
        }
        else
        {
            if (hitText != null)
            {
                hitText.text = "";
            }
        }
    }
}
