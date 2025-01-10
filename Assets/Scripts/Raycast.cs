using UnityEngine;
using TMPro; // TextMeshPro için gerekli namespace

public class Raycast : MonoBehaviour
{
    public TextMeshProUGUI hitText; // TextMeshProUGUI referansý

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Baþlangýçta hitText'in boþ olduðundan emin olun
        if (hitText != null)
        {
            hitText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ekranýn ortasýndan bir ray oluþtur
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Eðer ray bir objeye çarparsa
        if (Physics.Raycast(ray, out hit))
        {
            // Çarpýlan objenin ismini TextMeshPro'ya yazdýr
            if (hitText != null)
            {
                hitText.text = hit.collider.gameObject.name;
            }
        }
        else
        {
            // Eðer hiçbir objeye çarpmazsa, TextMeshPro'yu temizle
            if (hitText != null)
            {
                hitText.text = "";
            }
        }
    }
}
