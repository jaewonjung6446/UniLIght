using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending_manager : MonoBehaviour
{
    [SerializeField] private List<string> destexts;
    [SerializeField] private float textInterval;
    [SerializeField] private Text displayText;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject boom;
    [SerializeField] private GameObject Ignore_mission;
    private int num = 0;

    public Ending ending;
    private Fade fade;
    private Stack_Manager stack;
    public enum Ending
    {
        None,           //엔딩조건 없음
        depressive_A,   //엔딩1
        drug_A,         //엔딩2
        depressive_B,   //엔딩3
        love_B,         //엔딩4
        boom_fail,      //엔딩5
        heal_fail,      //엔딩6
        final_1,        //엔딩7
        final_2         //엔딩8
    }

    void Start()
    {
        //ending = Ending.None;
        fade = FindObjectOfType<Fade>();
        stack = FindObjectOfType<Stack_Manager>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        // 씬이 로드될 때 호출되는 이벤트에 함수 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        // 씬 이름이 "Day3"인지 확인

        if (scene.name == "Day3" && ending != Ending.None)
        {
            // Day3 씬이면 코루틴 시작
            fade.Fadeload("EndingScene");
        }
        else if (scene.name == "Day3" && ending == Ending.None)
        {
            StartCoroutine(UpdateText());
            displayText.gameObject.SetActive(true);
        }
    }
    public IEnumerator UpdateText()
    {
        Debug.Log("셜명 시작");
        yield return new WaitForSeconds(1.3f); // WaitForSeconds로 변경
        if (stack.check_map && stack.send_msg)
            Ignore_mission.SetActive(true);
        while (num <= destexts.Count)
        {
            if (num < destexts.Count)
            {
                if ((stack.check_map && stack.send_msg && (num == 10 || num == 13)) || (num != 10 && num != 13))
                {
                    Panel.SetActive(true);
                    displayText.text = destexts[num];
                    yield return new WaitForSeconds(textInterval); // WaitForSeconds로 변경
                    displayText.text = "";
                    yield return new WaitForSeconds(0.5f); // WaitForSeconds로 변경
                }
            }
            if (num == destexts.Count)
            {
                Debug.Log("셜명 완료");
                displayText.text = "";
                Panel.SetActive(false);
                boom.layer = 0;
                if(stack.check_map && stack.send_msg)
                    Ignore_mission.layer = 0;   //만약 히든 조건 달성시 망치 활성화
            }
            num++;
        }
    }
}

