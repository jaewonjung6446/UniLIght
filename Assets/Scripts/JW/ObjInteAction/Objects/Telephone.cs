using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour
{
    private MedicalInstruction MedicalInstruction;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MedicalInstruction = FindObjectOfType<MedicalInstruction>();
            MedicalInstruction.enabled = true;//���޻��� �����ϴ� ��ũ��Ʈ, �� �ذ�Ǹ� enable = false�� ���� �� ��.
        }
    }
}
