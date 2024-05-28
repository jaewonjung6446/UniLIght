using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Antidepressants : MonoBehaviour, Obj_Interface
{
    [SerializeField] private Text displayText; // �ؽ�Ʈ�� ����� UI �ؽ�Ʈ ������Ʈ
    [SerializeField] private Text des;
    [SerializeField] private GameObject Panel;
    private List<string> texts = new List<string> { "ù ��° �ؽ�Ʈ", "�� ��° �ؽ�Ʈ", "�� ��° �ؽ�Ʈ" }; // ����� �ؽ�Ʈ ����Ʈ
    private int currentTextIndex = -1; // ���� �ؽ�Ʈ �ε��� (�ʱ�ȭ)
    private bool isDisplayingTexts = false; // �ؽ�Ʈ ��� ������ ����
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && InterAction_Ctrl.Instance.hitObject.name == "Antidepressants")
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
        des.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);
        displayText.gameObject.SetActive(true);
        Debug.Log("�ٷ� ����");
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
        }
        else
        {
            EndTextSequence();
        }
    }

    void EndTextSequence()
    {
        des.gameObject.SetActive(true);
        Panel.gameObject.SetActive(false);
        Time.timeScale = 1; // ���� �簳
        isDisplayingTexts = false; // �ؽ�Ʈ ��� ����
        currentTextIndex = -1; // �ε��� �ʱ�ȭ
        displayText.gameObject.SetActive(false);
    }
}
