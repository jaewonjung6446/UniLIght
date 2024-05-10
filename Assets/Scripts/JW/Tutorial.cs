using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image DesImage;
    private void Start()
    {
        StartCoroutine(StartScene());
    }
    IEnumerator StartScene()
    {
        if(Resources.Load<Sprite>("Images/B_a") == null)
        {
            Debug.Log("이미지 불러오기 실패");
        }

        DesImage.sprite = Resources.Load<Sprite>("Images/B_a");
        yield return new WaitForSecondsRealtime(2.5f);
        DesImage.sprite = Resources.Load<Sprite>("Images/B_b");
        yield return new WaitForSecondsRealtime(2.5f);
        DesImage.enabled = false;
        SceneManager.LoadScene("Jaewon_Test1");
    }
}
