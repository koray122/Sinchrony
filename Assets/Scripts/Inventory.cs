using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // Envanterdeki öðeleri tutan liste

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
        item.SetActive(false); // Envantere eklenen objeyi sahnede görünmez yap
        Debug.Log("Envantere eklendi: " + item.name); // Konsola envantere eklendiðini yazdýr
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
                items.Remove(existingItem); // Miktar sýfýr veya daha az ise öðeyi envanterden çýkar
                item.SetActive(true); // Envanterden çýkarýlan objeyi sahnede görünür yap
                Debug.Log("Envanterden çýkarýldý: " + item.name); // Konsola envanterden çýkarýldýðýný yazdýr
            }
        }
    }

    public List<InventoryItem> GetItems()
    {
        return items; // Envanterdeki öðelerin listesini döndür
    }
}