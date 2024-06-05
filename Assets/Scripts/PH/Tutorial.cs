using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Tutorial : MonoBehaviour
{
    // [SerializeField] private Image DesImage;
    [SerializeField] private Image[] tutorialImages;
    [SerializeField] private string[] tutorialTexts;
    [SerializeField] private Text tutorialTextUI; // �ؽ�Ʈ�� ǥ���� UI Text ������Ʈ
    [SerializeField] private string[] controlTexts; // ���۹� �ؽ�Ʈ �迭
    [SerializeField] private Text controlTextUI; // ���۹� �ؽ�Ʈ�� ǥ���� UI Text
    [SerializeField] private GameObject panel; // �ؽ�Ʈ�� �̹����� ǥ���� �г�
    [SerializeField] private Sprite tornImage;
    [SerializeField] private Image controlImage; //���۹�
    private int currentControlTextIndex = 0;

    public CanvasGroup imageCanvasGroup; // CanvasGroup ������Ʈ�� �Ҵ��� ����
    public float fadeInTime = 0.5f; // ���̵��ο� �ɸ��� �ð�
    public float fadeOutTime = 0.5f; // ���̵�ƿ��� �ɸ��� �ð�
    public float startDelay = 1.5f; // ���̵��� ���� �� ��� �ð�
    public float displayTime = 2.5f; // �� �̹����� �ؽ�Ʈ�� ǥ�õǴ� �ð�

    public string sceneName;
    private Fade fade;
    private Coroutine tutorialCoroutine;

    private void Start()
    {
        fade = FindObjectOfType<Fade>();
        foreach ( Image image in tutorialImages )
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }

        Color color_c = controlImage.color;
        color_c.a = 0f;
        controlImage.color = color_c;

        if (controlImage == null)
        {
            Debug.LogError("Instruction Image is not assigned in the inspector.");
            return; // Prevent further execution if the image is not assigned
        }

        tutorialCoroutine = StartCoroutine(StartScene());
        imageCanvasGroup.alpha = 0;
        // StartCoroutine(FadeInAndOut());
        //DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
    }
    IEnumerator StartScene()
    {
        if (Resources.Load<Sprite>("Images/B_a") == null)
        {
            Debug.Log("�̹��� �ҷ����� ����");
        }

        // ESC Ű �Է��� ����ϴ� �ڷ�ƾ ����
        StartCoroutine(WaitForSkip());

        // �� �̹����� ���������� ���̵���
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            yield return StartCoroutine(FadeInImageAndText(tutorialImages[i]), tutorialTexts[i]);
            yield return new WaitForSecondsRealtime(displayTime);
        }

        // ������ �̹����� ������ �̹����� ����
        Image lastImage = tutorialImages[tutorialImages.Length - 1];
        FadeInImage(lastImage); // �����̴� ���� ���� -> startcoroutine()����
        lastImage.sprite = tornImage;
        yield return new WaitForSecondsRealtime(displayTime);

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

        // Ʃ�丮�� ��ȭ ���� �Ѿ�� ���� ���۹� ���� �̹���
        // ���۹� �̹����� ���̵���
        yield return StartCoroutine(FadeInImageAndText(controlImage));

        // ����ڰ� Ŭ���� ������ ���
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        //�� ��ȯ
        fade.Fadeload("Yeonggyo_test");
        // StartCoroutine("LoadSceneAndDeactivate");
    }

    IEnumerator FadeInImageAndText(Image image, string text)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        tutorialTextUI.text = text; // �ؽ�Ʈ ����
        tutorialTextUI.color = new Color(tutorialTextUI.color.r, tutorialTextUI.color.g, tutorialTextUI.color.b, 0f); // �ؽ�Ʈ �����ϰ� ����
        Color textColor = tutorialTextUI.color;

        panel.SetActive(true); // �г� Ȱ��ȭ

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

    IEnumerator SkipToInstructions()
    {
        // ��� Ʃ�丮�� �̹����� ��Ȱ��ȭ
        foreach (Image image in tutorialImages)
        {
            image.enabled = false;
        }

        // ���� �̹����� ���̵���
        yield return StartCoroutine(FadeInImage(controlImage));

        // ����ڰ� Ŭ���� ������ ���
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // �� ��ȯ
        fade.Fadeload("Yeonggyo_test");
    }

    IEnumerator WaitForSkip()
    {
        // ESC Ű�� ���� ������ ���
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Escape));
        if (tutorialCoroutine != null)
        {
            StopCoroutine(tutorialCoroutine);           
            StartCoroutine(SkipToInstructions());
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
