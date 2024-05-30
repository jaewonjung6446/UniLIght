using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    public List<Item> items = new List<Item>();
    public int maxInventorySize = 20;
    public void Awake()
    {
        inventory = this;
    }
    public bool AddItem(Item item)
    {
        if (items.Count >= maxInventorySize)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        items.Add(item);
        Debug.Log(item.itemName + " added to inventory.");
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.itemName + " removed from inventory.");
            return true;
        }

        Debug.Log("Item not found in inventory.");
        return false;
    }

    public void PrintInventory()
    {
        foreach (var item in items)
        {
            Debug.Log("Item: " + item.itemName);
        }
    }
}
