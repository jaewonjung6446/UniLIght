using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Item_Picture : MonoBehaviour, IItem
{
    public string Name => "Picture";
    public Sprite Icon => Resources.Load<Sprite>("Images/»çÁø");
    public void Use()
    {

    }
}
