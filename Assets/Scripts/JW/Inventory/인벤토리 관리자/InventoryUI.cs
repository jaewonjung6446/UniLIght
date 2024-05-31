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
        foreach(var a in sprites)
        {
            // 새로운 GameObject를 생성하고 이름을 스프라이트 이름으로 설정합니다.
            GameObject back = new GameObject(a.name);

            // SpriteRenderer 컴포넌트를 추가하고 스프라이트를 설정합니다.
            SpriteRenderer spriteRenderer = back.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = a;

            // 부모 스프라이트 'c'를 포함하는 새로운 GameObject를 생성합니다.
            GameObject parentSpriteObj = new GameObject("ParentSprite");
            SpriteRenderer parentSpriteRenderer = parentSpriteObj.AddComponent<SpriteRenderer>();
            parentSpriteRenderer.sprite = backGround;

            // 생성된 'back' 오브젝트를 'parentSpriteObj'의 자식으로 설정합니다.
            back.transform.SetParent(parentSpriteObj.transform);
            parentSpriteObj.transform.SetParent(InventoryUIImage.transform);

            // parentSpriteObj를 월드 위치로 이동하여 모든 스프라이트가 보이도록 설정합니다.
            parentSpriteObj.transform.position = Vector3.zero;

        }
    }
    public void DisplayInventory()
    {
        foreach (Sprite sprite in sprites)
        {
            // Image 프리팹을 인스턴스화합니다.
            GameObject imageObject = Instantiate(imagePrefab, parentTransform);
            if (imageObject != null) Debug.Log(imageObject.name);
            imageObject.AddComponent<SpriteRenderer>();
            // Image 컴포넌트를 가져와 스프라이트를 설정합니다.
            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.sprite = sprite;
        }
    }
}
