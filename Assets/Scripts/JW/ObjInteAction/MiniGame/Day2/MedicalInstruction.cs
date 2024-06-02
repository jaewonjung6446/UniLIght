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
    public StringInterpolationFromInspector stringInterpolationFromInspector;
    public int medicalIndex;
    private PlayMedical playMedical;
    private int a = 0;
    private string s_instruction;
    private bool Instruction = false;
    public short getInstructionCount = 0;
    private void Start()
    {
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
            yield return new WaitForSecondsRealtime(textInterval);
        }
    }
    public IEnumerator PlayInstruction()
    {
        while (Instruction)
        {
            s_instruction = "";
            getInstructionCount++;
            if (getInstructionCount == goal&& playMedical.newinstruction)
            {
                Debug.Log("게임 클리어");
                Instruction = false;
                instructions.text = "클리어";
                break;
            }
            if (playMedical.newinstruction)
            {
                playMedical.newinstruction = false;
                Debug.Log("다음 지시,instructions.text");
                medicalIndex = Random.Range(0, medicalObj.Count);
                gpt.SendUrgentRequest(medicalObj[medicalIndex]);
                //instructions.text = GetString(Random.Range(0, stringInterpolationFromInspector.WhatToSay.Count), medicalIndex);
                //medicalIndex = Random.Range(0, medicalObj.Count);
                //instructions.text = GetString(Random.Range(0, stringInterpolationFromInspector.WhatToSay.Count), medicalIndex);
                yield return new WaitForSecondsRealtime(instructionInterval);
            }
        }
    }
    //나머지 문장들의 인덱스, 물체이름의 인덱스를 대입하면 문장 자동 완성하는 메커니즘
    public string GetString(int elseStringIndex, int medicalObjIndex)
    {
        for (int i = 0; i < stringInterpolationFromInspector.WhatToSay[elseStringIndex].stringInfo.Count; i++)
        {
            if (i == stringInterpolationFromInspector.WhatToSay[elseStringIndex].insertPoint)
            {
                s_instruction += medicalObj[medicalObjIndex];
            }
            s_instruction += stringInterpolationFromInspector.WhatToSay[elseStringIndex].stringInfo[i];
        }
        return s_instruction;
    }
}
