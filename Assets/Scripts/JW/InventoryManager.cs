using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // 인벤토리를 저장할 List
    private List<GameObject> inventory = new List<GameObject>();

    // GameObject를 인벤토리에 추가하는 메소드
    public void AddToInventory(GameObject item)
    {
        if (item == null)
        {
            Debug.LogWarning("AddToInventory called with null item.");
            return;
        }

        // 인벤토리 리스트에 아이템 추가
        inventory.Add(item);

        // 아이템을 인벤토리에 추가했다는 것을 로그로 남김
        Debug.Log(item.name + " has been added to the inventory.");

        // 선택적으로, 아이템을 비활성화 할 수 있습니다. (인벤토리에 보관되므로)
        item.SetActive(false);
    }

    // 인벤토리의 내용을 콘솔에 출력하는 메소드
    public void PrintInventory()
    {
        foreach (GameObject item in inventory)
        {
            Debug.Log("Inventory Item: " + item.name);
        }
    }
}
