using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Antidepressants : MonoBehaviour, Obj_Interface
{
    [SerializeField] private Text displayText; // 텍스트를 출력할 UI 텍스트 오브젝트
    [SerializeField] private Text des;
    [SerializeField] private GameObject Panel;
    private List<string> texts = new List<string> { "첫 번째 텍스트", "두 번째 텍스트", "세 번째 텍스트" }; // 출력할 텍스트 리스트
    private int currentTextIndex = -1; // 현재 텍스트 인덱스 (초기화)
    private bool isDisplayingTexts = false; // 텍스트 출력 중인지 여부
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
        Debug.Log("줄력 시작");
        Time.timeScale = 0; // 게임 일시정지
        isDisplayingTexts = true; // 텍스트 출력 시작
        currentTextIndex = -1; // 인덱스 초기화
        DisplayNextText(); // 첫 텍스트 출력
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
        Time.timeScale = 1; // 게임 재개
        isDisplayingTexts = false; // 텍스트 출력 종료
        currentTextIndex = -1; // 인덱스 초기화
        displayText.gameObject.SetActive(false);
    }
}
