using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_ending : MonoBehaviour
{
    // 바뀔 Sprite들을 담을 배열
    public Sprite[] sprites;

    // Image 컴포넌트 참조
    private Image image;

    // 현재 Sprite를 결정하는 변수
    public int currentSpriteIndex;

    private GameObject ending;
    Ending_manager end;

    void Start()
    {
        // Canvas의 자식인 Image 컴포넌트 찾기
        image = GetComponentInChildren<Image>();
        ending = GameObject.Find("Ending_Manager"); //다른 씬의 오브젝트도 검색 가능한지 처음 앎;
        end = ending.GetComponent<Ending_manager>();

        if (image == null)
        {
            Debug.LogError("No Image component found in children!");
            return;
        }

        // 초기 Sprite 설정
        SelectEnding();
        Show_end();
    }

    void Update()
    {
    }

    // Sprite를 업데이트하는 함수
    void SelectEnding()
    {
        if (end.ending == Ending_manager.Ending.None)
        {
            Debug.Log("Error, there's no ending trigger");
        }
        else if(end.ending == Ending_manager.Ending.depressive_A)
        {
            end1();
        }
        else if (end.ending == Ending_manager.Ending.drug_A)
        {
            end2();
        }
        else if (end.ending == Ending_manager.Ending.depressive_B)
        {
            end3();
        }
        else if (end.ending == Ending_manager.Ending.love_B)
        {
            end4();
        }
        else if (end.ending == Ending_manager.Ending.boom_fail)
        {
            end5();
        }
        else if (end.ending == Ending_manager.Ending.heal_fail)
        {
            end6();
        }
        else if (end.ending == Ending_manager.Ending.final_1)
        {
            end7();
        }
        else if (end.ending == Ending_manager.Ending.final_2)
        {
            end8();
        }
        else
        {
            Debug.Log("Error, something wrong");
        }
    }

    private void end1() //우울_A
    {
        //그림과 텍스트 설정
        image.sprite = sprites[0];
    }
    private void end2() //약물
    {

    }
    private void end3() //우울_B
    {

    }
    private void end4() //집착
    {

    }
    private void end5() //미니게임A 실패
    {


    }
    private void end6() //미니게임B 실패
    {

    }
    private void end7() //포격 엔딩
    {

    }
    private void end8() //명령불복종 엔딩
    {

    }
    private void Show_end() //이미지와 텍스트 출력 및 컨트롤
    {

    }
}
