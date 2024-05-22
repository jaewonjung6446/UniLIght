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
    public string sceneName;

    private void Start()
    {
        StartCoroutine(StartScene());
        imageCanvasGroup.alpha = 0;
        StartCoroutine(FadeInAndOut());
        DontDestroyOnLoad(this.gameObject);
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
        StartCoroutine("LoadSceneAndDeactivate");
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
    IEnumerator LoadSceneAndDeactivate()
    {
        // ���� �񵿱������� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // ���� �ε�� ������ ��ٸ�
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ���� �ε�� �� ���� ��Ʈ ������Ʈ�� ��Ȱ��ȭ
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.IsValid())
        {
            SetActiveSceneObjects(loadedScene, false);
        }
        else
        {
            Debug.LogError("�� �ε� ����: " + sceneName);
        }
    }
    public void SetActiveSceneObjects(Scene scene, bool isActive)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            if (!go.activeSelf)
            {
                Debug.Log("Ʈ�� ��ȯ");
            }
            go.SetActive(isActive);
        }
    }
    public void ActivateSpecificObject(Scene scene, string objectName)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            if (go.name == objectName)
            {
                go.SetActive(true);
                break;
            }
        }
    }

    public void ActivateScene()
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            SetActiveSceneObjects(scene, true);
        }
    }

    public void DeactivateScene()
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            SetActiveSceneObjects(scene, false);
        }
    }
}
