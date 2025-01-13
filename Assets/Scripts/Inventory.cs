using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // Envanterdeki öðeleri tutan liste
    public InventoryUI inventoryUI; // Envanter UI referansý
    private bool inventoryVisible = false; // Envanterin görünür olup olmadýðýný takip eder
    private List<GameObject> inventoryObjects = new List<GameObject>(); // Inventory tag'ine sahip tüm nesneleri saklayan liste

    void Start()
    {
        // Oyun baþýnda Inventory tag'ine sahip tüm nesneleri bul ve listeye ekle
        inventoryObjects.AddRange(GameObject.FindGameObjectsWithTag("Inventory"));

        // Baþlangýçta tüm inventory nesnelerini kapat
        foreach (GameObject obj in inventoryObjects)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        // M tuþuna basýldýðýnda envanter nesnelerinin görünürlüðünü deðiþtir
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleInventoryVisibility();
        }
    }

    public void AddItem(GameObject item)
    {
        // Envanterde ayný isimde bir öðe olup olmadýðýný kontrol et
        InventoryItem existingItem = items.Find(i => i.item.name == item.name);
        if (existingItem != null)
        {
            existingItem.quantity++; // Ayný isimde öðe varsa miktarýný artýr
        }
        else
        {
            items.Add(new InventoryItem(item, 1)); // Ayný isimde öðe yoksa yeni bir öðe ekle
        }

        // Envantere eklenen objeyi görünmez yap
        item.SetActive(false);

        Debug.Log("Envantere eklendi: " + item.name); // Konsola bilgi yazdýr
        inventoryUI.UpdateInventoryUI(); // Envanter UI'sini güncelle
    }

    public void RemoveItem(GameObject item)
    {
        // Envanterde ayný isimde bir öðe olup olmadýðýný kontrol et
        InventoryItem existingItem = items.Find(i => i.item.name == item.name);
        if (existingItem != null)
        {
            existingItem.quantity--; // Ayný isimde öðe varsa miktarýný azalt
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem); // Miktar sýfýrsa öðeyi envanterden çýkar
                item.SetActive(true); // Sahnede görünür yap
                Debug.Log("Envanterden çýkarýldý: " + item.name);
            }

            inventoryUI.UpdateInventoryUI(); // Envanter UI'sini güncelle
        }
    }

    public List<InventoryItem> GetItems()
    {
        return items; // Envanterdeki öðelerin listesini döndür
    }

    private void ToggleInventoryVisibility()
    {
        // inventoryVisible deðiþkenini tersine çevir
        inventoryVisible = !inventoryVisible;

        // Listedeki tüm nesnelerin görünürlük durumunu deðiþtir
        foreach (GameObject obj in inventoryObjects)
        {
            obj.SetActive(inventoryVisible);
        }
    }
}
