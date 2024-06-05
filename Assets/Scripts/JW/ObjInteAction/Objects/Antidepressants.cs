using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Antidepressants : MonoBehaviour, Obj_Interface
{
    [SerializeField] private Text displayText; // 텍스트를 출력할 UI 텍스트 오브젝트
    [SerializeField] private Text des;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Image antidepressants;

    private List<string> texts = new List<string> { "나른해지고 아무 생각이 들지 않게 된다.\n복용 하시겠습니까? \n (E를 눌러 복용/Q로 취소)", "조금은 진정이 된다" }; // 출력할 텍스트 리스트
    private int currentTextIndex = -1; // 현재 텍스트 인덱스 (초기화)
    private bool isDisplayingTexts = false; // 텍스트 출력 중인지 여부

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
        Debug.Log("출력 시작");
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
            if (currentTextIndex == 0)
            {
                antidepressants.gameObject.SetActive(true);
                if (Resources.Load<Sprite>("Images/antidepressant") != null)
                {
                    antidepressants.sprite = Resources.Load<Sprite>("Images/antidepressant");
                    if (antidepressants.sprite == Resources.Load<Sprite>("Images/antidepressant"))
                        Debug.Log("antidepressant 로드");
                }
                else
                {
                    Debug.Log("불러오기 실패");
                }
            }
            else if (currentTextIndex == 1)
            {
                antidepressants.sprite = Resources.Load<Sprite>("Images/swallow");
                stackmanager.subDep_A();
                stackmanager.AddDrug();
                antidepressants.sprite = Resources.Load<Sprite>("Images/swallow");
                Debug.Log("swallow 로드");
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
        Time.timeScale = 1; // 게임 재개
        isDisplayingTexts = false; // 텍스트 출력 종료
        currentTextIndex = -1; // 인덱스 초기화
        displayText.gameObject.SetActive(false);
        Gamemanager.Instance.StopAvilable = true;

    }
    void Start()
    {
        stack = GameObject.Find("StackManager");
        stackmanager = stack.GetComponent<Stack_Manager>();
    }
}
