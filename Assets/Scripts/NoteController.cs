using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class NoteController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode closeKey; // Not kapama tuþu
    [Space(10)]
    [SerializeField] private GameObject player; // Oyuncu objesi (CharacterController içermeli)

    [Header("UI Text")]
    [SerializeField] private GameObject noteCanvas; // Not UI paneli
    [SerializeField] private TMP_Text noteTextAreaUI; // Notun yazýlacaðý UI Text alaný

    [Space(10)]
    [SerializeField][TextArea] private string noteText; // Gösterilecek not metni

    [Space(10)]
    [SerializeField] private UnityEvent openEvent; // Not açýldýðýnda çalýþtýrýlacak event
    private bool isOpen = false; // Notun açýk olup olmadýðýný takip eder

    public void ShowNote()
    {
        // Notu göster
        noteTextAreaUI.text = noteText;
        noteCanvas.SetActive(true);
        openEvent.Invoke();
        DisablePlayer(true); // Oyuncu hareketini devre dýþý býrak
        isOpen = true;
    }

    public void HideNote()
    {
        DisableNote();
    }

    private void DisableNote()
    {
        // Notu kapat
        noteCanvas.SetActive(false);
        DisablePlayer(false); // Oyuncu hareketini etkinleþtir
        isOpen = false;
    }

    private void DisablePlayer(bool disable)
    {
        // Oyuncunun hareketini devre dýþý býrak/etkinleþtir
        var characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = !disable;
        }
        else
        {
            Debug.LogWarning("Player object does not have a CharacterController component!");
        }
    }

    private void Update()
    {
        // Not açýksa ve kapama tuþuna basýlýrsa notu kapat
        if (isOpen && Input.GetKeyDown(closeKey))
        {
            DisableNote();
        }
    }
}
