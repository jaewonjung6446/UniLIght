using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Picture1 : MonoBehaviour, Obj_Interface
{
    private int DesImageIndex = -1;
    [SerializeField] private List<Sprite> DesImages;
    [SerializeField] private Image DesImage;
    [SerializeField] private Text displayText;
    [SerializeField] private GameObject Panel;

    private GameObject stack;
    Stack_Manager stackmanager;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DesImage.gameObject.activeSelf)
            {
                StartDisplayImage();
            }
            else
            {
                ShowNextImage();
            }
        }
        if (DesImage.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Q))
        {
            EndImage();
        }
    }
    void StartDisplayImage()
    {
        Gamemanager.Instance.StopAvilable = false;

        displayText.gameObject.SetActive(true);
        Time.timeScale = 0; // ���� �Ͻ�����
        DesImage.enabled = true; // �ؽ�Ʈ ��� ����
        DesImageIndex = -1; // �ε��� �ʱ�ȭ
        ShowNextImage(); // ù �ؽ�Ʈ ���
        Panel.gameObject.SetActive(true);
    }
    void ShowNextImage()
    {
        DesImageIndex++;

        if (DesImageIndex < DesImages.Count)
        {
            InterAction_Ctrl.Instance.DesTextAvailable = false;
            displayText.text = "������ ���� ���� �����̴�";
            DesImage.gameObject.SetActive(true);
            DesImage.sprite = DesImages[DesImageIndex];
        }
        else
        {
            EndImage();
        }
    }
    void EndImage()
    {
        Time.timeScale = 1; // ���� �簳
        InterAction_Ctrl.Instance.DesTextAvailable = true;
        DesImage.gameObject.SetActive(false); // �ؽ�Ʈ ��� ����
        DesImageIndex = -1; // �ε��� �ʱ�ȭ
        displayText.gameObject.SetActive(false);
        Panel.gameObject.SetActive(false);
        Inventory.inventory.AddItem(this.gameObject);
        this.gameObject.SetActive(false);
        Gamemanager.Instance.StopAvilable = true;

    }

    void Start()
    {
        stack = GameObject.Find("StackManager");
        stackmanager = stack.GetComponent<Stack_Manager>();
    }
}