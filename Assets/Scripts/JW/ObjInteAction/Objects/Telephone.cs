using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour, Obj_Interface
{
    [SerializeField] GameObject MedicalInstruction;
    private MedicalInstruction medical;
    private void OnEnable()
    {
        medical = MedicalInstruction.GetComponent<MedicalInstruction>();
    }
    public void InterAction()
    {
        if (medical.Instruction)
        {
            Debug.Log("�� �̻� ��ȭ�⿡�� ������ ����");
        }
        if (Input.GetKeyDown(KeyCode.E)&& !medical.Instruction)
        {
            MedicalInstruction.SetActive(true);//���޻��� �����ϴ� ��ũ��Ʈ, �� �ذ�Ǹ� enable = false�� ���� �� ��.
        }
    }
}
