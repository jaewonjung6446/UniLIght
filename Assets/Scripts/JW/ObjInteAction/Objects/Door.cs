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
    private Fade fade;
    private void Start()
    {
        playMedical = medicalBox.GetComponent<PlayMedical>();
        instruction = medicalInstruction.GetComponent<MedicalInstruction>();
        fade = FindObjectOfType<Fade>();
        if (fade != null)
        {
            Debug.Log("Fade서치 완료");
        }

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
                    Tinstruction.text = "성공";
                    playMedical.issucceeded = true;
                    playMedical.newinstruction = true;
                }
                else
                {
                    Tinstruction.text = "실패";
                    fade.Fadeload("EndingScene");
                    playMedical.issucceeded = false;
                    playMedical.newinstruction = true;
                }
            }
            else
            {
                Tinstruction.text = "아직 때가 아니다";
            }
        }
    }
    private IEnumerator TextOnOff()
    {
        Tinstruction.text = "잘했어, 빨리 이리 줘";
        yield return new WaitForSecondsRealtime(2.0f);
        Tinstruction.text = "";
    }
}
