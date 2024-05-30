using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Antidepressants : MonoBehaviour,IItem
{
    public string Name => "Antidepressants";
    public Sprite Icon => Resources.Load<Sprite>("Images/antidepressant");
    public void Use()
    {

    }
}
