using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour,Obj_Interface
{
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("�κ��丮 �ν�");
            Inventory.inventory.AddItem(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
