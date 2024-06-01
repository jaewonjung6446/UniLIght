using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayJoystick : MonoBehaviour
{
    [SerializeField] private Text instructions;
    [SerializeField] private List<string> destexts;
    [SerializeField] private Text CountDown;
    [SerializeField] private float interval;
    [SerializeField] private int fontSize;
    [SerializeField] private float minCall;
    [SerializeField] private float maxCall;
    [SerializeField] private List<GameObject> callObjs;
    [SerializeField] private float timing;
    [SerializeField] private float sceneInterval;
    private Ending_manager ending_Manager;
    private bool startGame = false;
    private bool countdown = false;
    private bool waitforClick = true;
    private bool isPass = false;
    private GameObject callObj;
    private int a = 0;
    private int b = 0;
    private int call = 0;
    private void OnEnable()
    {
        startGame = false;
        countdown = false;
        waitforClick = true;
        callObj = null;
        isPass = false;
        a = 0;
        b = 0;
        call = 0;
        CountDown.text = "";
        instructions.text = "";
        ending_Manager = FindObjectOfType<Ending_manager>();
        StartCoroutine(UpdateText());
    }
    private void Update()
    {
        if (countdown)
        {
            StartCoroutine(Countdown());
        }
        if (startGame)
        {
            StartCoroutine(PlayJoystickGame());
        }
        //Debug.Log("입력대기상태 : " + waitforClick);
        if (!waitforClick)
        {
            CountDown.text = "게임오버";
            StartCoroutine(GameOver());
            StopCoroutine(PlayJoystickGame());
        }
    }
    public IEnumerator UpdateText()
    {
        Debug.Log("설명 시작");
        while (a <= destexts.Count)
        {
            if (a < destexts.Count)
            {
                instructions.text = destexts[a];
            }
            if (a == destexts.Count)
            {
                Debug.Log("카운트 다운 시작");
                countdown = true;
                instructions.text = "";
            }
            a++;
            yield return new WaitForSecondsRealtime(interval);
        }
    }
    public IEnumerator Countdown()
    {
        countdown = false;
        while (b <= 3)
        {
            if (b < 3)
            {
                CountDown.text = (b + 1).ToString();
                CountDown.fontSize = 80 + b * fontSize;
            }
            if (b == 3)
            {
                Debug.Log("게임 시작");
                CountDown.text = "";
                startGame = true;
                CountDown.fontSize = 80;
                CursorManager.Instance.UnlockCursor();
            }
            b++;
            yield return new WaitForSecondsRealtime(1.0f);
        }

    }
    IEnumerator PlayJoystickGame()
    {
        startGame = false;
        for (int a = 0; a <= 5; a++)
        {
            float timeInterval = Random.Range(minCall, maxCall);
            if (a < 5)
            {
                call = Random.Range(0, callObjs.Count);
                instructions.text = "오브젝트" + (call + 1).ToString();
                callObj = callObjs[call];
                callObj.GetComponent<Button>().image.color = Color.red;
                StartCoroutine(GetPressClick());
            }
            if (a == 5)
            {
                CountDown.text = "클리어, ESC로 복귀";
                Cursor.lockState = CursorLockMode.Locked;
            }
            yield return new WaitForSecondsRealtime(timeInterval);
        }
    }
    IEnumerator GetPressClick()
    {
        waitforClick = true;
        yield return new WaitForSecondsRealtime(timing);
        waitforClick = false;
        if (isPass)
        {
            waitforClick = true;
        }
        isPass = false;
        callObj.GetComponent<Button>().image.color = Color.white;
    }
    public void PressButton(int a)
    {
        Debug.Log("호출 인덱스 = "+(call+1));
        Debug.Log(a);

        if (a == (call+1) && waitforClick)
        {
            Debug.Log("통과");
            isPass = true;

        }
        else if(!waitforClick)
        {
            StartCoroutine(GameOver());
            Debug.Log("게임오버");
            CountDown.text = "게임오버";
            StopCoroutine(PlayJoystickGame());
            isPass = false;
        }
        else if(a != (call+1))
        {
            StartCoroutine(GameOver());
            Debug.Log("게임오버");
            CountDown.text = "게임오버";
            StopCoroutine(PlayJoystickGame());
            isPass = false;
        }
    }
    private IEnumerator GameOver()
    {
        ending_Manager.ending = Ending_manager.Ending.boom_fail;
        yield return new WaitForSecondsRealtime(sceneInterval);
        SceneManager.LoadScene("EndingScene");
    }
}
