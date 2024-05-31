using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] Sprite backGround;
    [SerializeField] private GameObject InventoryUIImage;
    public GameObject imagePrefab;        
    public Transform parentTransform;
    private List<GameObject> ItemsUI;
    private List<Sprite> sprites = new List<Sprite>();
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void UpdateUI()
    {
        MakeUI(GetSprite(inventory.items));
    }
    public List<Sprite> GetSprite(List<GameObject> items)
    {
        foreach (var a in items)
        {
            if (a.GetComponent<IItem>().GetSprite() != null)
            {
                Debug.Log("IItem ����");
                if (!sprites.Contains(a.GetComponent<IItem>().GetSprite()))
                {
                    sprites.Add(a.GetComponent<IItem>().GetSprite());
                    Debug.Log("IItem ��������Ʈ �˻� ����");
                }
            }
        }
        return sprites;
    }
    private void MakeUI(List<Sprite> sprites)
    {
        foreach(var a in sprites)
        {
            // ���ο� GameObject�� �����ϰ� �̸��� ��������Ʈ �̸����� �����մϴ�.
            GameObject back = new GameObject(a.name);

            // SpriteRenderer ������Ʈ�� �߰��ϰ� ��������Ʈ�� �����մϴ�.
            SpriteRenderer spriteRenderer = back.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = a;

            // �θ� ��������Ʈ 'c'�� �����ϴ� ���ο� GameObject�� �����մϴ�.
            GameObject parentSpriteObj = new GameObject("ParentSprite");
            SpriteRenderer parentSpriteRenderer = parentSpriteObj.AddComponent<SpriteRenderer>();
            parentSpriteRenderer.sprite = backGround;

            // ������ 'back' ������Ʈ�� 'parentSpriteObj'�� �ڽ����� �����մϴ�.
            back.transform.SetParent(parentSpriteObj.transform);
            parentSpriteObj.transform.SetParent(InventoryUIImage.transform);

            // parentSpriteObj�� ���� ��ġ�� �̵��Ͽ� ��� ��������Ʈ�� ���̵��� �����մϴ�.
            parentSpriteObj.transform.position = Vector3.zero;

        }
    }
    public void DisplayInventory()
    {
        foreach (Sprite sprite in sprites)
        {
            // Image �������� �ν��Ͻ�ȭ�մϴ�.
            GameObject imageObject = Instantiate(imagePrefab, parentTransform);
            if (imageObject != null) Debug.Log(imageObject.name);
            imageObject.AddComponent<SpriteRenderer>();
            // Image ������Ʈ�� ������ ��������Ʈ�� �����մϴ�.
            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.sprite = sprite;
        }
    }
}
