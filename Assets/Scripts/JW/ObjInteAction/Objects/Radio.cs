using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour, Obj_Interface
{
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
            }
        }
    }
}
