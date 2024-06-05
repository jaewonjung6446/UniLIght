using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Picture2 : MonoBehaviour, IItem
{
    public string Name => "Picture2";
    public Sprite Icon => Resources.Load<Sprite>("Images/»çÁø");
    public void Use()
    {

    }
}

