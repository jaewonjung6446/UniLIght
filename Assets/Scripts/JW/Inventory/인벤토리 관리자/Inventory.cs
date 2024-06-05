using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    [SerializeField] private InventoryUI InventoryUI;
    public GameObject text;
    public GameObject image;
    public List<GameObject> items = new List<GameObject>();
    public GameObject InventoryAll;
    public bool isUI = false;
    public bool isCancelAvailable = false;
    private void Start()
    {
        inventory = this;
        //FindAllItems();
        ListItems();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isUI)
        {
            isCancelAvailable = true;
            float b = image.GetComponent<Image>().color.a;
            b = 1;

            Debug.Log("UI활성화");
            isUI = true;
            Time.timeScale = 0.0001f;
            InventoryAll.SetActive(true);
            Gamemanager.Instance.StopAvilable = false;
            CursorManager.Instance.UnlockCursor();
        }else if (Input.GetKeyDown(KeyCode.Q) && isUI && isCancelAvailable)
        {
            isCancelAvailable = false;
            Debug.Log("UI비활성화");
            isUI = false;
            Time.timeScale = 1;
            InventoryAll.SetActive(false);
            Gamemanager.Instance.StopAvilable = true;
            CursorManager.Instance.LockCursor();
            text.GetComponent<Text>().text = "";
            float b = image.GetComponent<Image>().color.a;
            b = 0;
        }
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
