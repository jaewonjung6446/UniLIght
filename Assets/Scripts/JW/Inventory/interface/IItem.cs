using UnityEngine;
public enum ItemInfo
{
    Item_Picture//���� ������Ʈ
}
public interface IItem
{
    public string Name { get; }
    public Sprite Icon { get;}
    public void Use();
    public Sprite GetSprite()
    {
        return Icon;
    }
    public string GetName() {
        return Name;
    }
}
