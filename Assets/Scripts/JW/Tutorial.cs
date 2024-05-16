using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image DesImage;
    public CanvasGroup imageCanvasGroup; // CanvasGroup 컴포넌트를 할당할 변수
    public float fadeInTime = 0.5f; // 페이드인에 걸리는 시간
    public float fadeOutTime = 0.5f; // 페이드아웃에 걸리는 시간
    public float startDelay = 1.5f; // 페이드인 시작 전 대기 시간

    private void Start()
    {
        StartCoroutine(StartScene());
        imageCanvasGroup.alpha = 0;
        StartCoroutine(FadeInAndOut());
    }
    IEnumerator StartScene()
    {
        if (Resources.Load<Sprite>("Images/B_a") == null)
        {
            Debug.Log("이미지 불러오기 실패");
        }

        DesImage.sprite = Resources.Load<Sprite>("Images/B_a");
        yield return new WaitForSecondsRealtime(3.5f);
        DesImage.sprite = Resources.Load<Sprite>("Images/B_b");
        yield return new WaitForSecondsRealtime(2.5f);
        DesImage.enabled = false;
        SceneManager.LoadScene("Jaewon_Test1");
    }
    IEnumerator FadeInAndOut()
    {
        yield return new WaitForSeconds(startDelay); // 특정 시간 대기
        Debug.Log("페이드인");
        // 페이드인 처리
        float elapsedTime = 0f;
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeInTime;
            imageCanvasGroup.alpha = alpha;
            yield return null;
        }
        imageCanvasGroup.alpha = 1; // 최종적으로 완전히 불투명하게 설정
        yield return new WaitForSecondsRealtime(1.0f);
        // 페이드아웃 처리
        elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeOutTime);
            imageCanvasGroup.alpha = alpha;
            yield return null;
        }
        imageCanvasGroup.alpha = 0; // 최종적으로 완전히 투명하게 설정
    }
}
