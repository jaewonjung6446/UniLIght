using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    [SerializeField] GameObject MedicalInstruction;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MedicalInstruction.SetActive(true);//���޻��� �����ϴ� ��ũ��Ʈ, �� �ذ�Ǹ� enable = false�� ���� �� ��.
        }
    }
}
