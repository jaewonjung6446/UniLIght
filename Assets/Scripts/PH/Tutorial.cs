using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    // [SerializeField] private Image DesImage;
    [SerializeField] private Image[] tutorialImages;
    [SerializeField] private Sprite tornImage;
    public CanvasGroup imageCanvasGroup; // CanvasGroup ������Ʈ�� �Ҵ��� ����
    public float fadeInTime = 0.5f; // ���̵��ο� �ɸ��� �ð�
    public float fadeOutTime = 0.5f; // ���̵�ƿ��� �ɸ��� �ð�
    public float startDelay = 1.5f; // ���̵��� ���� �� ��� �ð�
    public string sceneName;
    private Fade fade;
    private void Start()
    {
        fade = FindObjectOfType<Fade>();
        foreach ( Image image in tutorialImages )
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }
        StartCoroutine(StartScene());
        imageCanvasGroup.alpha = 0;
        // StartCoroutine(FadeInAndOut());
        DontDestroyOnLoad(this.gameObject);
    }
    IEnumerator StartScene()
    {
        if (Resources.Load<Sprite>("Images/B_a") == null)
        {
            Debug.Log("�̹��� �ҷ����� ����");
        }

        // �� �̹����� ���������� ���̵���
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            yield return StartCoroutine(FadeInImage(tutorialImages[i]));
            yield return new WaitForSecondsRealtime(2.5f);
        }

        // ������ �̹����� ������ �̹����� ����
        Image lastImage = tutorialImages[tutorialImages.Length - 1];
        FadeInImage(lastImage); // �����̴� ���� ���� -> startcoroutine()����
        lastImage.sprite = tornImage;
        yield return new WaitForSecondsRealtime(2.5f);

        // ���������� ���̵�ƿ�
        foreach (Image image in tutorialImages)
        {
            yield return StartCoroutine(FadeOutImage(image));
        }
        yield return new WaitForSecondsRealtime(2.5f); // ���̵�ƿ� ��� 


        //DesImage.enabled = false; //desimage��Ȱ��ȭ
        // Ʃ�丮�� �̹����� ��Ȱ��ȭ
        foreach(Image image in tutorialImages)
        {
            image.enabled = false;
        }
        //�� ��ȯ
        fade.Fadeload("Yeonggyo_test");
        // StartCoroutine("LoadSceneAndDeactivate");
    }

    IEnumerator FadeInImage(Image image)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeInTime);
            image.color = color;
            yield return null;
        }
        color.a = 1f;
        image.color = color;
    }

    IEnumerator FadeOutImage(Image image)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeOutTime));
            image.color = color;
            yield return null;
        }
        color.a = 0f;
        image.color = color;
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
