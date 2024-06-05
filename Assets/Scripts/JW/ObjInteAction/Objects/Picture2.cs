using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture2 : MonoBehaviour,Obj_Interface
{
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.inventory.AddItem(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
