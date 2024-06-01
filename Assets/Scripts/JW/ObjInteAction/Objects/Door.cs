using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour, Obj_Interface
{
    [SerializeField] private Text Tinstruction;
    [SerializeField] private GameObject medicalBox;
    [SerializeField] GameObject medicalInstruction;
    private PlayMedical playMedical;
    private MedicalInstruction instruction;
    private void Start()
    {
        playMedical = medicalBox.GetComponent<PlayMedical>();
        instruction = medicalInstruction.GetComponent<MedicalInstruction>();
    }
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TextOnOff());
            if (medicalInstruction.activeSelf)
            {
                if (playMedical.isCorrect)
                {
                    Tinstruction.text = "����";
                    playMedical.issucceeded = true;
                    playMedical.newinstruction = true;
                }
                else
                {
                    Tinstruction.text = "����";
                    playMedical.issucceeded = false;
                    playMedical.newinstruction = true;
                }
            }
            else
            {
                Tinstruction.text = "���� ���� �ƴϴ�";
            }
        }
    }
    private IEnumerator TextOnOff()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        Tinstruction.text = "";
    }
}
