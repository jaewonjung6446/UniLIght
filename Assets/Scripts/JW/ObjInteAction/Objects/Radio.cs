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
            // ������� ��� ���̸� ����, ���� ���¶�� ���
            if (InterAction_Ctrl.Instance.audioSource.isPlaying)
            {
                Time.timeScale = 1.0f; // ���� �Ͻ�����
                Debug.Log("����� ��� ����");
                InterAction_Ctrl.Instance.audioSource.Stop();
            }
            else
            {
                Time.timeScale = 0; // ���� �Ͻ�����
                InterAction_Ctrl.Instance.audioSource.Play();
                Debug.Log("����� ��� ����");
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
