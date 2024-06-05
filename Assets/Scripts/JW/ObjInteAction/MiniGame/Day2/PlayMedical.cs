using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMedical : MonoBehaviour
{
    [SerializeField] private GameObject medicalinstruction;
    [SerializeField] Text instruction;
    public bool newinstruction = false;
    private MedicalInstruction medicalInstruction;
    public int getButtonIndex;
    public bool isCorrect = false;
    public bool issucceeded = true;
    private void OnEnable()
    {
        medicalInstruction = medicalinstruction.GetComponent<MedicalInstruction>();
        CursorManager.Instance.UnlockCursor();
        instruction.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        CursorManager.Instance.LockCursor();
    }
    public void GetIsCorrect(int index)
    {
        if (medicalInstruction.medicalIndex == index)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
        if (medicalinstruction.activeSelf)
        {
            Debug.Log("전달 전, 예측 성공여부 = " + isCorrect);
        }
        else
        {
            StartCoroutine(MedicalText());
            Debug.Log("지시 전");
        }
    }
    private IEnumerator MedicalText()
    {
        instruction.text = "함부로 만지지마";
        yield return new WaitForSecondsRealtime(1.5f);
        instruction.text = "";
    }
}
