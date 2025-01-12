using UnityEngine;

[System.Serializable] // Bu sýnýfýn Unity tarafýndan serileþtirilebilir olduðunu belirtir
public class InventoryItem
{
    public GameObject item; // Envanterdeki öðeyi temsil eden GameObject
    public int quantity; // Bu öðeden envanterde kaç tane olduðunu belirten miktar

    // Constructor: Yeni bir InventoryItem oluþturur
    public InventoryItem(GameObject item, int quantity)
    {
        this.item = item; // Öðeyi ayarlar
        this.quantity = quantity; // Miktarý ayarlar
    }
}