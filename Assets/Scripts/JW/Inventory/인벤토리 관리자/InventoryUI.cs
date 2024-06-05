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
    private List<GameObject> itemsUI = new List<GameObject>(); // 소문자로 변경 및 초기화
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
        sprites.Clear(); // 기존의 sprites 목록을 초기화하여 중복을 방지
        foreach (var a in items)
        {
            if (a.GetComponent<IItem>().GetSprite() != null)
            {
                Debug.Log("IItem 접근");
                if (!sprites.Contains(a.GetComponent<IItem>().GetSprite()))
                {
                    sprites.Add(a.GetComponent<IItem>().GetSprite());
                    Debug.Log("IItem 스프라이트 검색 성공");
                }
            }
        }
        return sprites;
    }

    private void MakeUI(List<Sprite> sprites)
    {
        // 기존 UI 요소를 제거
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

            itemsUI.Add(parentSpriteObj); // 추가된 오브젝트를 목록에 저장
        }
    }

    public void DisplayInventory()
    {
        // 기존 UI 요소를 제거
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
            itemsUI.Add(imageObjectAll); // 생성된 오브젝트를 목록에 추가
        }
    }
}
