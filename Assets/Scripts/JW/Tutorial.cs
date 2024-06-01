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

    private void Start()
    {
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

        //// ���������� ���̵���
        //foreach (Image image in tutorialImages)
        //{
        //    yield return StartCoroutine(FadeInImage(image));
        //    yield return new WaitForSecondsRealtime(2.5f);
        //}

        // �� �̹����� ���������� ���̵���
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            yield return StartCoroutine(FadeInImage(tutorialImages[i]));
            yield return new WaitForSecondsRealtime(2.5f);
        }

        //DesImage.sprite = Resources.Load<Sprite>("Images/B_a");
        //yield return new WaitForSecondsRealtime(3.5f);
        //DesImage.sprite = Resources.Load<Sprite>("Images/B_b");
        //yield return new WaitForSecondsRealtime(2.5f);
        //DesImage.enabled = false;

        // ������ �̹����� ������ �̹����� ����
        Image lastImage = tutorialImages[tutorialImages.Length - 1];
        yield return StartCoroutine(FadeInImage(lastImage));
        // yield return new WaitForSecondsRealtime(2.5f);
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
        // SceneManager.LoadScene("Yeonggyo_test");
        // StartCoroutine("LoadSceneAndDeactivate");
    }
    //IEnumerator FadeInAndOut(Image image)
    //{
    //    yield return new WaitForSeconds(startDelay); // Ư�� �ð� ���
    //    Debug.Log("���̵���");
    //    // ���̵��� ó��
    //    float elapsedTime = 0f;
    //    while (elapsedTime < fadeInTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float alpha = elapsedTime / fadeInTime;
    //        imageCanvasGroup.alpha = alpha;
    //        yield return null;
    //    }
    //    imageCanvasGroup.alpha = 1; // ���������� ������ �������ϰ� ����
    //    yield return new WaitForSecondsRealtime(1.0f);
    //    // ���̵�ƿ� ó��
    //    elapsedTime = 0f;
    //    while (elapsedTime < fadeOutTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float alpha = 1 - (elapsedTime / fadeOutTime);
    //        imageCanvasGroup.alpha = alpha;
    //        yield return null;
    //    }
    //    imageCanvasGroup.alpha = 0; // ���������� ������ �����ϰ� ����
    //    yield return StartCoroutine(FadeInImage(image));
    //    yield return new WaitForSecondsRealtime(1.0f);
    //    yield return StartCoroutine(FadeOutImage(image));
    //}
    IEnumerator FadeInImage(Image image)
    {
        //float elapsedTime = 0f;
        //while (elapsedTime < fadeInTime)
        //{
        //    elapsedTime += Time.deltaTime;
        //    imageCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInTime);
        //    yield return null;
        //}
        //imageCanvasGroup.alpha = 1f;
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
        //float elapsedTime = 0f;
        //while (elapsedTime < fadeOutTime)
        //{
        //    elapsedTime += Time.deltaTime;
        //    imageCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeOutTime));
        //    yield return null;
        //}
        //imageCanvasGroup.alpha = 0f;

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
