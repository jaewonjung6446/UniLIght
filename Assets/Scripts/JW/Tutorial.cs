using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image DesImage;
    public CanvasGroup imageCanvasGroup; // CanvasGroup ������Ʈ�� �Ҵ��� ����
    public float fadeInTime = 0.5f; // ���̵��ο� �ɸ��� �ð�
    public float fadeOutTime = 0.5f; // ���̵�ƿ��� �ɸ��� �ð�
    public float startDelay = 1.5f; // ���̵��� ���� �� ��� �ð�

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
            Debug.Log("�̹��� �ҷ����� ����");
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
        yield return new WaitForSeconds(startDelay); // Ư�� �ð� ���
        Debug.Log("���̵���");
        // ���̵��� ó��
        float elapsedTime = 0f;
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = elapsedTime / fadeInTime;
            imageCanvasGroup.alpha = alpha;
            yield return null;
        }
        imageCanvasGroup.alpha = 1; // ���������� ������ �������ϰ� ����
        yield return new WaitForSecondsRealtime(1.0f);
        // ���̵�ƿ� ó��
        elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeOutTime);
            imageCanvasGroup.alpha = alpha;
            yield return null;
        }
        imageCanvasGroup.alpha = 0; // ���������� ������ �����ϰ� ����
    }
}
