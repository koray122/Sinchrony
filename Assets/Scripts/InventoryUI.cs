using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro için gerekli namespace

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI itemNamesText; // Envanterdeki öðelerin isimlerini gösterecek TextMeshPro bileþeni
    public TextMeshProUGUI itemQuantitiesText; // Envanterdeki öðelerin miktarlarýný gösterecek TextMeshPro bileþeni
    public Inventory inventory; // Envanter referansý

    void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory referansý atanmadý!"); // Inventory referansýnýn atanýp atanmadýðýný kontrol et
        }
        UpdateInventoryUI(); // Envanter UI'sini güncelle
    }

    public void UpdateInventoryUI()
    {
        Debug.Log("Updating Inventory UI"); // Debug mesajý ekleyin

        // Envanterdeki öðelerin isimlerini ve miktarlarýný toplamak için stringler
        string itemNames = "";
        string itemQuantities = "";

        // Envanterdeki öðeleri UI'de göster
        foreach (InventoryItem inventoryItem in inventory.GetItems())
        {
            itemNames += inventoryItem.item.name + "\n"; // Nesnenin ismini ekle
            itemQuantities += inventoryItem.quantity.ToString() + "\n"; // Nesnenin miktarýný ekle
        }

        // TextMeshPro bileþenlerini güncelle
        itemNamesText.text = itemNames;
        itemQuantitiesText.text = itemQuantities;
    }
}