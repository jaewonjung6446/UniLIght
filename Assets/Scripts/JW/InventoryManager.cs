using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // �κ��丮�� ������ List
    private List<GameObject> inventory = new List<GameObject>();

    // GameObject�� �κ��丮�� �߰��ϴ� �޼ҵ�
    public void AddToInventory(GameObject item)
    {
        if (item == null)
        {
            Debug.LogWarning("AddToInventory called with null item.");
            return;
        }

        // �κ��丮 ����Ʈ�� ������ �߰�
        inventory.Add(item);

        // �������� �κ��丮�� �߰��ߴٴ� ���� �α׷� ����
        Debug.Log(item.name + " has been added to the inventory.");

        // ����������, �������� ��Ȱ��ȭ �� �� �ֽ��ϴ�. (�κ��丮�� �����ǹǷ�)
        item.SetActive(false);
    }

    // �κ��丮�� ������ �ֿܼ� ����ϴ� �޼ҵ�
    public void PrintInventory()
    {
        foreach (GameObject item in inventory)
        {
            Debug.Log("Inventory Item: " + item.name);
        }
    }
}
