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

    // 텍스트 컴포넌트 참조
    public Text endingText;

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
        endingText.text = "자신이 수많은 생명들을 앗아가고 있다는 사실에 힘겨워하던 A,\n순간의 충동과 자기 혐오를 제어하지 못하고 자신이 위치하고 있는 군부대를 폭격하게 된다.";
    }
    private void end2() //약물
    {
        image.sprite = sprites[1];
        endingText.text = "전쟁 중의 우울감을 약물 복용으로 억제하던 A였지만,\n항정신성 약물의 오남용으로 인해 심각한 부작용을 초래하게 되고 결국 사망하게 된다.";
    }
    private void end3() //우울_B
    {
        image.sprite = sprites[2];
        endingText.text = "매일같이 고생했음에도 자신이 살리지 못한 여러 생명들이 존재한다는 사실에 힘겨워하던 B는\n밤마다 자신을 향해 원혼들이 소리치는 악몽을 꾸는 등 상태가 악화되어 가다가, 결국 실성하게 된다.";
    }
    private void end4() //집착
    {
        image.sprite = sprites[3];
        endingText.text = "보내질 리 없는 문자 메시지를 A에게 보내는 것으로 자신의 고충을 해소하던 B였으나,\n그녀가 전송하려고 시도했던 문자 기록이 군 통신망에 적발되게 되었고\n그녀는 군법 위반으로 구속된다.";
    }
    private void end5() //미니게임A 실패
    {
        image.sprite = sprites[4];
        endingText.text = "정확한 타이밍에 폭격을 하지 못한 A는 전략상 큰 실책을 하게 되고, 아군들에게 큰 손실을 입히게 된다.\n그간 우수하게 임무를 수행하던 A였기에 지휘관은 큰 질책을 하지는 않았지만,\n동료들을 위험에 빠지게 했다는 사실과 전쟁으로 많은 사람들을 죽이는 일에 대한 회의감 사이에서 괴리감을 겪고 있던 A는\n이번 일로 크게 충격을 받고 그에게 있어 평생의 트라우마로 남게 되었다…";

    }
    private void end6() //미니게임B 실패
    {
        image.sprite = sprites[5];
        endingText.text = "올바른 응급처치를 하지 못한 B는 생명을 구하지 못하게 된다.\n같이 일하는 동료들은 충분히 일어날 수 있는 일이고 너의 잘못이 아니라며 다독이지만,\n그녀는 크게 충격을 받고 이 일은 그녀에게 있어 평생의 트라우마로 남게 되었다..";
    }
    private void end7() //포격 엔딩
    {
        image.sprite = sprites[6];
        endingText.text = "최후의 고민 끝에 결국 병원을 폭격하게 된 A.\n불행 중 다행으로 B는 그 당시 외근을 나가 있어서 폭격에 휘말리지는 않았지만,\n그 사실을 알 리가 만무한 A는 부상자들을 폭격으로 죽였다는 사실과\n그의 손으로 B를 죽였다는 죄책감을 이겨내지 못하고 스스로 목숨을 끊는다.";
    }
    private void end8() //명령불복종 엔딩
    {
        image.sprite = sprites[7];
        endingText.text = "최후의 고민 끝에 결국 병원을 폭격하지 못한 A,\n비록 B를 살렸다는 것과 병원을 폭격한다는 비인륜적인 행위를 저지르지 않았다는 점에서\n심적인 안도감은 있었지만 그는 결국 군법 위반, 명령 불복종 사유로 군 감옥에 수감되게 된다.";
    }
    private void Show_end() //이미지와 텍스트 출력 및 컨트롤
    {
        
    }
}
