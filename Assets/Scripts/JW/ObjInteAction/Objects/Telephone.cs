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
            Debug.Log("더 이상 전화기에는 볼일이 없다");
        }
        if (Input.GetKeyDown(KeyCode.E)&& !medical.Instruction)
        {
            MedicalInstruction.SetActive(true);//응급상자 지시하는 스크립트, 다 해결되면 enable = false로 구현 할 것.
        }
    }
}
