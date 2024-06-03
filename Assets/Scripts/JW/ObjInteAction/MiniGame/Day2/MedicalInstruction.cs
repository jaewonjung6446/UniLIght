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

    private void Start()
    {
        StartCoroutine(UpdateText());
        Debug.Log("Ȱ��ȭ");
        instructions.gameObject.SetActive(true);
        playMedical = Medical.GetComponent<PlayMedical>();
    }

    public IEnumerator UpdateText()
    {
        Debug.Log("Day2 �̴ϰ��� ���� ����");
        while (a <= destexts.Count)
        {
            if (a < destexts.Count)
            {
                instructions.text = destexts[a];
            }
            if (a == destexts.Count)
            {
                Debug.Log("���� ����");
                instructions.text = "";
                Instruction = true;
                StartCoroutine(PlayInstruction());
            }
            a++;
            yield return new WaitForSeconds(textInterval); // WaitForSeconds�� ����
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
                Debug.Log("���� Ŭ����");
                Instruction = false;
                instructions.text = "Ŭ����";
                break;
            }
            if (getInstructionCount < goal)
            {
                playMedical.newinstruction = false;
                Debug.Log("���� ����, instructions.text");
                medicalIndex = Random.Range(0, medicalObj.Count);
                yield return StartCoroutine(gpt.SendUrgentRequest(medicalObj[medicalIndex]));
            }

            yield return new WaitForSeconds(instructionInterval); // WaitForSeconds�� ����
        }
    }
}
