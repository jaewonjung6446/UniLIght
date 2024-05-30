using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<IItem> items = new List<IItem>();

    private void Start()
    {
        //FindAllItems();
        ListItems();
    }
    // Add an item to the inventory
    public void AddItem(IItem item)
    {
        items.Add(item);
        Debug.Log($"{item.Name} added to the inventory.");
    }

    // Remove an item from the inventory
    public void RemoveItem(IItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log($"{item.Name} removed from the inventory.");
        }
        else
        {
            Debug.Log($"{item.Name} is not in the inventory.");
        }
    }

    // List all items in the inventory
    public void ListItems()
    {
        Debug.Log("Items in the inventory:");
        foreach (var item in items)
        {
            Debug.Log(item.Name);
        }
    }

    // Find all classes that implement IItem and return them as a list
    public List<IItem> FindAllItems()
    {
        //List<IItem> itemList = new List<IItem>();

        var itemType = typeof(IItem);
        var types = Assembly.GetAssembly(itemType).GetTypes()
            .Where(t => itemType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                // Instantiate MonoBehaviour derived classes using AddComponent
                IItem item = (IItem)gameObject.AddComponent(type);
                //itemList.Add(item);
                items.Add(item);
            }
            else
            {
                // Instantiate non-MonoBehaviour derived classes using Activator
                IItem item = (IItem)Activator.CreateInstance(type);
                //itemList.Add(item);
                items.Add(item);
            }
        }

        //return itemList;
        return items;
    }
}
