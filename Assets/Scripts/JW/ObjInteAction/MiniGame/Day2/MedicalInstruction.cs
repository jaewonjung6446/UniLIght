using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalInstruction : MonoBehaviour
{
    [SerializeField] private List<string> destexts;
    [SerializeField] private List<string> medicalObj;
    public Text instructions;
    [SerializeField] private float textInterval;
    [SerializeField] private float instructionInterval;
    [SerializeField] private short goal;
    [SerializeField] private GameObject Medical;
    public GPT4Request gpt;
    public int medicalIndex;
    private PlayMedical playMedical;
    private int a = 0;
    private string s_instruction;
    public bool Instruction = false;
    public short getInstructionCount = 0;
    private Fade fade;
    private Ending_manager ending_Manager;

    private void Start()
    {
        ending_Manager = FindObjectOfType<Ending_manager>();
        fade = FindObjectOfType<Fade>();
        if (fade != null)
        {
            Debug.Log("Fade서치 완료");
        }
        StartCoroutine(UpdateText());
        Debug.Log("활성화");
        instructions.gameObject.SetActive(true);
        playMedical = Medical.GetComponent<PlayMedical>();
    }
    public IEnumerator UpdateText()
    {
        Debug.Log("Day2 미니게임 설명 시작");
        while (a <= destexts.Count)
        {
            if (a < destexts.Count)
            {
                instructions.text = destexts[a];
            }
            if (a == destexts.Count)
            {
                Debug.Log("게임 시작");
                instructions.text = "";
                Instruction = true;
                StartCoroutine(PlayInstruction());
            }
            a++;
            yield return new WaitForSeconds(textInterval); // WaitForSeconds로 변경
        }
    }

    public IEnumerator PlayInstruction()
    {
        while (Instruction)
        {
            s_instruction = "";
            getInstructionCount++;
            Debug.Log("Instruction Count: " + getInstructionCount);
            if (getInstructionCount == goal && playMedical.newinstruction)
            {
                Debug.Log("게임 클리어");
                Instruction = false;
                instructions.text = "클리어";
                fade.Fadeload("Day3");
                break;
            }
            if (getInstructionCount < goal)
            {
                if (!playMedical.issucceeded)
                {
                    Debug.Log("실패, 엔딩 출력");
                    Instruction = false;
                    ending_Manager.ending = Ending_manager.Ending.heal_fail;
                    fade.Fadeload("EndingScene");
                }
                playMedical.newinstruction = false;
                Debug.Log("다음 지시, instructions.text");
                medicalIndex = Random.Range(0, medicalObj.Count);
                yield return StartCoroutine(gpt.SendUrgentRequest(medicalObj[medicalIndex]));
            }

            yield return new WaitForSeconds(instructionInterval); // WaitForSeconds로 변경
            if(playMedical.isCorrect == false)
            {
                Debug.Log("실패, 엔딩 출력");
                Instruction = false;
                ending_Manager.ending = Ending_manager.Ending.heal_fail;
                fade.Fadeload("EndingScene");
            }
            yield return null;
        }
    }
}
