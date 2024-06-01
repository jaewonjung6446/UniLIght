using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalInstruction : MonoBehaviour
{
    [SerializeField] private List<string> destexts;
    [SerializeField] private List<string> medicalObj;
    [SerializeField] private Text instructions;
    [SerializeField] private float textInterval;
    [SerializeField] private float instructionInterval;
    [SerializeField] private short maxCount;
    public StringInterpolationFromInspector stringInterpolationFromInspector;
    private int a = 0;
    private string s_instruction;
    private bool Instruction = false;
    private short getInstructionCount = 0;
    private void Start()
    {
        StartCoroutine(UpdateText());
        Debug.Log("Ȱ��ȭ");
        instructions.gameObject.SetActive(true);
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
            yield return new WaitForSecondsRealtime(textInterval);
        }
    }
    public IEnumerator PlayInstruction()
    {
        while (Instruction)
        {
            s_instruction = "";
            getInstructionCount++;
            instructions.text = GetString(Random.Range(0, stringInterpolationFromInspector.WhatToSay.Count),Random.Range(0, medicalObj.Count));
            if (getInstructionCount == maxCount)
            {
                Debug.Log("���� Ŭ����");
                instructions.text = "Ŭ����";
                break;
            }
            yield return new WaitForSecondsRealtime(instructionInterval);
        }
    }
    //������ ������� �ε���, ��ü�̸��� �ε����� �����ϸ� ���� �ڵ� �ϼ��ϴ� ��Ŀ����
    public string GetString(int elseStringIndex,int medicalObjIndex)
    {
        for(int  i =0; i< stringInterpolationFromInspector.WhatToSay[elseStringIndex].stringInfo.Count; i++)
        {
            if(i == stringInterpolationFromInspector.WhatToSay[elseStringIndex].insertPoint)
            {
                s_instruction += medicalObj[medicalObjIndex];
            }
            s_instruction += stringInterpolationFromInspector.WhatToSay[elseStringIndex].stringInfo[i];
        }
        return s_instruction;
    }
}
