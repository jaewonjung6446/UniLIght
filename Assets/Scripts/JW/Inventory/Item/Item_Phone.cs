using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Telephone : MonoBehaviour,IItem
{
    public string Name => "Phone";
    public Sprite Icon => Resources.Load<Sprite>("Images/»çÁø");
    public void Use()
    {

    }

}
