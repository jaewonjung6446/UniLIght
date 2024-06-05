using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Show_ending : MonoBehaviour
{
    [System.Serializable]
    public class EndingData
    {
        public Sprite[] images; // 엔딩에 표시될 이미지 배열
        public string[] texts; // 각 이미지에 해당하는 텍스트 배열
    }

    // 총 8가지 엔딩 데이터를 담을 배열
    public EndingData[] endings;   // 바뀔 Sprite들을 담을 배열 
    [SerializeField] private Image image; // 엔딩 이미지를 표시할 UI Image
    [SerializeField] private Text endingText; // 엔딩 텍스트를 표시할 UI Text
    [SerializeField] private GameObject panel; // 엔딩을 표시할 패널
    // public float displayTime = 2.0f; // 각 이미지와 텍스트가 표시되는 시간

    private int currentEndingIndex; // 현재 엔딩 인덱스(sprite)
    private int currentImageIndex; // 현재 이미지 인덱스
    private bool isDisplayingTexts = false; // 텍스트 출력 중인지 여부

    private GameObject ending;
    Ending_manager end;
    private Fade fade;

    private void Start()
    {
        // Canvas의 자식인 Image 컴포넌트 찾기
        image = GetComponentInChildren<Image>();
        ending = GameObject.Find("Ending_Manager"); //다른 씬의 오브젝트도 검색 가능한지 처음 앎;
        end = ending.GetComponent<Ending_manager>();
        fade = FindObjectOfType<Fade>();

        if (end == null)
        {
            Debug.LogError("Ending_manager component not found on Ending_Manager object!");
            return;
        }

        if (image == null)
        {
            Debug.LogError("No Image component found in children!");
            return;
        }

        if (endingText == null)
        {
            Debug.LogError("endingText is not assigned in the inspector!");
            return;
        }

        // 초기 Sprite 설정
        SelectEnding();
    }

    void Update()
    {
        InterAction();
    }
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDisplayingTexts)
            {
                Show_end();
            }
            else
            {
                DisplayNextText();
            }
        }
    }

    // Sprite를 업데이트하는 함수
    private void SelectEnding()
    {
        if (end.ending == Ending_manager.Ending.None)
        {
            Debug.Log("Error, there's no ending trigger");
        }
        else if(end.ending == Ending_manager.Ending.depressive_A)
        {
            currentEndingIndex = 0;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.drug_A)
        {
            currentEndingIndex = 1;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.depressive_B)
        {
            currentEndingIndex = 2;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.love_B)
        {
            currentEndingIndex = 3;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.boom_fail)
        {
            currentEndingIndex = 4;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.heal_fail)
        {
            currentEndingIndex = 5;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.final_1)
        {
            currentEndingIndex = 6;
            Show_end();
        }
        else if (end.ending == Ending_manager.Ending.final_2)
        {
            currentEndingIndex = 7;
            Show_end();
        }
        else
        {
            Debug.Log("Error, something wrong");
        }
    }

    // 엔딩 텍스트 시퀀스를 시작하는 함수
    private void Show_end() //이미지와 텍스트 출력 및 컨트롤
    {
        isDisplayingTexts = true;
        currentImageIndex = 0;
        panel.SetActive(true);
        DisplayNextText();
    }

    // 다음 텍스트와 이미지를 표시하는 함수
    private void DisplayNextText()
    {
        EndingData ending = endings[currentEndingIndex];

        if (currentImageIndex < ending.images.Length)
        {
            image.sprite = ending.images[currentImageIndex];
            endingText.text = ending.texts[currentImageIndex];
            currentImageIndex++;
        }
        else
        {
            EndTextSequence();
        }
    }
    // 텍스트 시퀀스를 종료하는 함수
    private void EndTextSequence()
    {
        isDisplayingTexts = false;
        panel.SetActive(false);
        
        // 엔딩 이후 첫 씬으로 돌아가기.
        fade.Fadeload("StartScene");
    }

}
