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
    public Transform AllparentTransform;
    private List<GameObject> itemsUI = new List<GameObject>(); // �ҹ��ڷ� ���� �� �ʱ�ȭ
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
        sprites.Clear(); // ������ sprites ����� �ʱ�ȭ�Ͽ� �ߺ��� ����
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
        // ���� UI ��Ҹ� ����
        foreach (var item in itemsUI)
        {
            Destroy(item);
        }
        itemsUI.Clear();

        foreach (var a in sprites)
        {
            GameObject back = new GameObject(a.name);

            SpriteRenderer spriteRenderer = back.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = a;

            GameObject parentSpriteObj = new GameObject("ParentSprite");
            SpriteRenderer parentSpriteRenderer = parentSpriteObj.AddComponent<SpriteRenderer>();
            parentSpriteRenderer.sprite = backGround;

            back.transform.SetParent(parentSpriteObj.transform);
            parentSpriteObj.transform.SetParent(InventoryUIImage.transform);

            parentSpriteObj.transform.position = Vector3.zero;

            itemsUI.Add(parentSpriteObj); // �߰��� ������Ʈ�� ��Ͽ� ����
        }
    }

    public void DisplayInventory()
    {
        // ���� UI ��Ҹ� ����
        foreach (var item in itemsUI)
        {
            Destroy(item);
        }
        itemsUI.Clear();

        HashSet<Sprite> uniqueSprites = new HashSet<Sprite>(sprites);

        foreach (Sprite sprite in uniqueSprites)
        {
            GameObject imageObject = Instantiate(imagePrefab, parentTransform);
            GameObject imageObjectAll = Instantiate(imagePrefab, AllparentTransform);
            string s_name = sprite.name;
            Debug.Log(s_name);

            imageObject.AddComponent<SpriteRenderer>();
            imageObjectAll.AddComponent<Button>();
            imageObjectAll.AddComponent<UseManager>();
            imageObjectAll.GetComponent<UseManager>().name = s_name;

            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.sprite = sprite;
            Image imageComponentAll = imageObjectAll.GetComponent<Image>();
            imageComponentAll.sprite = sprite;

            itemsUI.Add(imageObject);
            itemsUI.Add(imageObjectAll); // ������ ������Ʈ�� ��Ͽ� �߰�
        }
    }
}
