using UnityEngine;
public enum ItemInfo
{
    Item_Picture//사진 오브젝트
}
public interface IItem
{
    string Name { get; }
    Sprite Icon { get;}
    public void Use();
}
