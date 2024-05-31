using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    [SerializeField] private InventoryUI InventoryUI;
    public List<GameObject> items = new List<GameObject>();

    private void Start()
    {
        inventory = this;
        //FindAllItems();
        ListItems();
    }
    // Add an item to the inventory
    public void AddItem(GameObject item)
    {
        if (item.GetComponent<IItem>() != null)
        {
            items.Add(item);
            Debug.Log($"{item.GetComponent<IItem>().Name} added to the inventory.");
            ListItems();
            InventoryUI.GetSprite(inventory.items);
            InventoryUI.DisplayInventory();
        }
    }

    // Remove an item from the inventory
    public void RemoveItem(GameObject item)
    {
        if (item.GetComponent<IItem>() != null)
        {
            items.Remove(item);
            Debug.Log($"{item.GetComponent<IItem>().Name} removed from the inventory.");
        }
        else
        {
            Debug.Log($"{item.GetComponent<IItem>().Name} is not in the inventory.");
        }
        ListItems();
        InventoryUI.GetSprite(inventory.items);
        InventoryUI.DisplayInventory();
    }

    // List all items in the inventory
    public void ListItems()
    {
        Debug.Log("Items in the inventory:");
        foreach (var item in items)
        {
            Debug.Log(item.GetComponent<IItem>().Name);
        }
    }
}
