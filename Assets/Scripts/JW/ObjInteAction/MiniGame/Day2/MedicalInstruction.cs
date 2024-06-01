using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalInstruction : MonoBehaviour
{
    [SerializeField] private List<string> destexts;
    [SerializeField] private List<string> medicalObj;
    [SerializeField] private List<string> instructionElse;
    [SerializeField] private Text instructions;
    [SerializeField] private float textInterval;
    [SerializeField] private float instructionInterval;
    [SerializeField] private short maxCount;
    public StringInterpolationFromInspector stringInterpolationFromInspector;
    private int a = 0;
    private string s_instruction;
    private bool Instruction = false;
    private short getInstructionCount = 0;
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
            getInstructionCount++;
            instructions.text = medicalObj[Random.Range(0, medicalObj.Count)];
            if (getInstructionCount == maxCount)
            {
                Debug.Log("게임 클리어");
                instructions.text = "클리어";
                break;
            }
            yield return new WaitForSeconds(instructionInterval);
        }
    }
    public string GetString()
    {
        int i_else = Random.Range(0, stringInterpolationFromInspector.WhatToSay.Count);
        for (int i = 0; i < stringInterpolationFromInspector.WhatToSay[i_else].stringInfo.Count; i++)
        {

        }
        //int i_else = Random.Range(0, instructionElse.Count);
        string a = stringInterpolationFromInspector.WhatToSay[0].stringInfo[0];


        return s_instruction;
    }
}
