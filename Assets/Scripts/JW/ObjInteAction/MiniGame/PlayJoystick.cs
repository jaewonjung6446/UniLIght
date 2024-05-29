using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayJoystick : MonoBehaviour
{
    [SerializeField] private Text instructions;
    [SerializeField] private List<string> texts;
    [SerializeField] private float interval;
    private bool startGame = false;
    private bool countdown = false;
    private int a = 0;
    private int b = 0;
    private void Start()
    {
        StartCoroutine(UpdateText());
    }
    private void Update()
    {
        if (startGame)
        {
            StartCoroutine(JoystickGame());
        }
    }
    public IEnumerator UpdateText()
    {
        while (a <= texts.Count)
        {
            if (a < texts.Count)
            {
                instructions.text = texts[a];
            }
            if (a == texts.Count)
            {
                Debug.Log("게임 시작");
                countdown = true;
                instructions.text = "";
            }
            a++;
            yield return new WaitForSecondsRealtime(interval);
        }
    }
    IEnumerator JoystickGame()
    {
        yield return null;
    }
}
