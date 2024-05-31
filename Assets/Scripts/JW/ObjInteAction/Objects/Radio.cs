using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ending_manager;

public class Radio : MonoBehaviour, Obj_Interface
{
    private GameObject stack;
    Stack_Manager stackmanager;

    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 오디오가 재생 중이면 중지, 중지 상태라면 재생
            if (InterAction_Ctrl.Instance.audioSource.isPlaying)
            {
                Time.timeScale = 1.0f; // 게임 일시정지
                Debug.Log("오디오 출력 중지");
                InterAction_Ctrl.Instance.audioSource.Stop();
            }
            else
            {
                Time.timeScale = 0; // 게임 일시정지
                InterAction_Ctrl.Instance.audioSource.Play();
                Debug.Log("오디오 출력 시작");
                stackmanager.AddDep_A();
            }
        }
    }

    public void Start()
    {
        InterAction_Ctrl.Instance.audioSource.Stop();
        stack = GameObject.Find("StackManager");
        stackmanager = stack.GetComponent<Stack_Manager>();
    }
}
