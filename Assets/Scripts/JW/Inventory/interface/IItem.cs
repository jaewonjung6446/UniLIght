using UnityEngine;
public enum ItemInfo
{
    Item_Picture//���� ������Ʈ
}
public interface IItem
{
    string Name { get; }
    Sprite Icon { get;}
    public void Use();
}
