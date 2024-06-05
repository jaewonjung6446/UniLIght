using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Antidepressants : MonoBehaviour, Obj_Interface
{
    [SerializeField] private Text displayText; // �ؽ�Ʈ�� ����� UI �ؽ�Ʈ ������Ʈ
    [SerializeField] private Text des;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Image antidepressants;

    private List<string> texts = new List<string> { "���������� �ƹ� ������ ���� �ʰ� �ȴ�.\n���� �Ͻðڽ��ϱ�? \n (E�� ���� ����/Q�� ���)", "������ ������ �ȴ�" }; // ����� �ؽ�Ʈ ����Ʈ
    private int currentTextIndex = -1; // ���� �ؽ�Ʈ �ε��� (�ʱ�ȭ)
    private bool isDisplayingTexts = false; // �ؽ�Ʈ ��� ������ ����

    private GameObject stack;
    Stack_Manager stackmanager;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (!isDisplayingTexts)
            {
                StartTextSequence();
            }
            else
            {
                DisplayNextText();
            }
        }
        if (isDisplayingTexts && Input.GetKeyDown(KeyCode.Q))
        {
            EndTextSequence();
        }
    }
    void StartTextSequence()
    {
        Gamemanager.Instance.StopAvilable = false;
        Panel.gameObject.SetActive(true);
        InterAction_Ctrl.Instance.DesTextAvailable = false;
        displayText.gameObject.SetActive(true);
        Debug.Log("��� ����");
        Time.timeScale = 0; // ���� �Ͻ�����
        isDisplayingTexts = true; // �ؽ�Ʈ ��� ����
        currentTextIndex = -1; // �ε��� �ʱ�ȭ
        DisplayNextText(); // ù �ؽ�Ʈ ���
    }

    void DisplayNextText()
    {
        currentTextIndex++;

        if (currentTextIndex < texts.Count)
        {
            displayText.text = texts[currentTextIndex];
            if (currentTextIndex == 0)
            {
                antidepressants.gameObject.SetActive(true);
                if (Resources.Load<Sprite>("Images/antidepressant") != null)
                {
                    antidepressants.sprite = Resources.Load<Sprite>("Images/antidepressant");
                    if (antidepressants.sprite == Resources.Load<Sprite>("Images/antidepressant"))
                        Debug.Log("antidepressant �ε�");
                }
                else
                {
                    Debug.Log("�ҷ����� ����");
                }
            }
            else if (currentTextIndex == 1)
            {
                antidepressants.sprite = Resources.Load<Sprite>("Images/swallow");
                stackmanager.subDep_A();
                stackmanager.AddDrug();
                antidepressants.sprite = Resources.Load<Sprite>("Images/swallow");
                Debug.Log("swallow �ε�");
            }
        }
        else
        {
            EndTextSequence();
        }
    }

    void EndTextSequence()
    {
        InterAction_Ctrl.Instance.DesTextAvailable = true;
        Panel.gameObject.SetActive(false);
        antidepressants.gameObject.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        isDisplayingTexts = false; // �ؽ�Ʈ ��� ����
        currentTextIndex = -1; // �ε��� �ʱ�ȭ
        displayText.gameObject.SetActive(false);
        Gamemanager.Instance.StopAvilable = true;

    }
    void Start()
    {
        stack = GameObject.Find("StackManager");
        stackmanager = stack.GetComponent<Stack_Manager>();
    }
}
