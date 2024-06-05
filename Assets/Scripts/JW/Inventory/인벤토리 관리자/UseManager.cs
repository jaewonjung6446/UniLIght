using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseManager : MonoBehaviour
{
    public string name;
    public Button myButton;
    public bool isClicked = false;
    [SerializeField] private GameObject textText;
    public GameObject image;
    private int currentIndex = 0;
    private Coroutine displayCoroutine;

    private void Awake()
    {
        image = GameObject.Find("InventoryImage");
        if (image != null)
        {
            SetImageAlpha(0); // 이미지를 투명하게 설정
        }

        myButton = GetComponent<Button>();
        textText = GameObject.Find("InventoryText");

        if (myButton != null)
            Debug.Log("버튼 서치");
    }

    public void Update()
    {
        if (!isClicked)
            myButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (this.name == "사진" && displayCoroutine == null)
        {
            Debug.Log(this.name);
            GlobalCoroutineRunner.StartGlobalCoroutine(Phone());
        }
        if (this.name == "antidepressant" && displayCoroutine == null)
        {
            Debug.Log(this.name);
            GlobalCoroutineRunner.StartGlobalCoroutine(DisplaySequence());
        }
        isClicked = false;
    }

    public IEnumerator Phone()
    {
        textText.GetComponent<Text>().text = "위치가 너무 깊은 지하여서 데이터가 제대로 작동하지 않아 무용지물이다.";
        yield return new WaitForSecondsRealtime(1.5f);
        textText.GetComponent<Text>().text = "";
        displayCoroutine = null;
    }

    private IEnumerator DisplaySequence()
    {
        Time.timeScale = 0;
        currentIndex = 0;
        Debug.Log(Gamemanager.Instance.images.Length);

        while (currentIndex < 2)
        {
            // 현재 이미지와 텍스트 표시
            image.GetComponent<Image>().sprite = Gamemanager.Instance.images[currentIndex];
            SetImageAlpha(1.0f); // 이미지를 보이게 설정
            Debug.Log("출력 시작");
            textText.GetComponent<Text>().text = Gamemanager.Instance.texts[currentIndex];

            // 키 입력 대기
            bool next = false;
            while (!next)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    next = true;
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    ClearDisplay();
                    yield break;
                }
                yield return null;
            }

            // 현재 이미지 숨김
            SetImageAlpha(0.0f); // 이미지를 다시 투명하게 설정
            currentIndex++;
        }

        // 모든 이미지와 텍스트가 끝났을 때
        ClearDisplay();
    }

    private void SetImageAlpha(float alpha)
    {
        if (image != null)
        {
            Image img = image.GetComponent<Image>();
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }

    private void ClearDisplay()
    {
        if (currentIndex < 2)
        {
            SetImageAlpha(0.0f); // 이미지를 투명하게 설정
        }
        textText.GetComponent<Text>().text = "";
        Time.timeScale = 1;
        displayCoroutine = null;
    }
}
