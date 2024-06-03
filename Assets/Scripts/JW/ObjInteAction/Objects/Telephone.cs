using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone : MonoBehaviour, Obj_Interface
{
    [SerializeField] GameObject MedicalInstruction;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MedicalInstruction.SetActive(true);//응급상자 지시하는 스크립트, 다 해결되면 enable = false로 구현 할 것.
        }
    }
}
