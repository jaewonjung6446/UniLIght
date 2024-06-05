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
    [SerializeField] private Text tutorialTextUI; // 텍스트를 표시할 UI Text 컴포넌트
    [SerializeField] private string[] controlTexts; // 조작법 텍스트 배열
    [SerializeField] private Text controlTextUI; // 조작법 텍스트를 표시할 UI Text
    [SerializeField] private GameObject panel; // 텍스트와 이미지를 표시할 패널
    [SerializeField] private Sprite tornImage;
    [SerializeField] private Image controlImage; //조작법
    private int currentControlTextIndex = 0;

    public CanvasGroup imageCanvasGroup; // CanvasGroup 컴포넌트를 할당할 변수
    public float fadeInTime = 0.5f; // 페이드인에 걸리는 시간
    public float fadeOutTime = 0.5f; // 페이드아웃에 걸리는 시간
    public float startDelay = 1.5f; // 페이드인 시작 전 대기 시간
    public float displayTime = 2.5f; // 각 이미지와 텍스트가 표시되는 시간

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
            Debug.Log("이미지 불러오기 실패");
        }

        // ESC 키 입력을 대기하는 코루틴 시작
        StartCoroutine(WaitForSkip());

        // 각 이미지를 순차적으로 페이드인
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            yield return StartCoroutine(FadeInImageAndText(tutorialImages[i]), tutorialTexts[i]);
            yield return new WaitForSecondsRealtime(displayTime);
        }

        // 마지막 이미지를 찢어진 이미지로 변경
        Image lastImage = tutorialImages[tutorialImages.Length - 1];
        FadeInImage(lastImage); // 깜빡이는 현상 수정 -> startcoroutine()삭제
        lastImage.sprite = tornImage;
        yield return new WaitForSecondsRealtime(displayTime);

        // 순차적으로 페이드아웃
        foreach (Image image in tutorialImages)
        {
            yield return StartCoroutine(FadeOutImage(image));
        }
        yield return new WaitForSecondsRealtime(2.5f); // 페이드아웃 대기 


        //DesImage.enabled = false; //desimage비활성화
        // 튜토리얼 이미지를 비활성화
        foreach(Image image in tutorialImages)
        {
            image.enabled = false;
        }

        // 튜토리얼 만화 이후 넘어가기 전에 조작법 설명 이미지
        // 조작법 이미지를 페이드인
        yield return StartCoroutine(FadeInImageAndText(controlImage));

        // 사용자가 클릭할 때까지 대기
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        //씬 전환
        fade.Fadeload("Yeonggyo_test");
        // StartCoroutine("LoadSceneAndDeactivate");
    }

    IEnumerator FadeInImageAndText(Image image, string text)
    {
        float elapsedTime = 0f;
        Color color = image.color;
        tutorialTextUI.text = text; // 텍스트 설정
        tutorialTextUI.color = new Color(tutorialTextUI.color.r, tutorialTextUI.color.g, tutorialTextUI.color.b, 0f); // 텍스트 투명하게 설정
        Color textColor = tutorialTextUI.color;

        panel.SetActive(true); // 패널 활성화

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
        // 씬을 비동기적으로 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // 씬이 로드될 때까지 기다림
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬이 로드된 후 씬의 루트 오브젝트를 비활성화
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);
        if (loadedScene.IsValid())
        {
            SetActiveSceneObjects(loadedScene, false);
        }
        else
        {
            Debug.LogError("씬 로드 실패: " + sceneName);
        }
    }

    IEnumerator SkipToInstructions()
    {
        // 모든 튜토리얼 이미지를 비활성화
        foreach (Image image in tutorialImages)
        {
            image.enabled = false;
        }

        // 지시 이미지를 페이드인
        yield return StartCoroutine(FadeInImage(controlImage));

        // 사용자가 클릭할 때까지 대기
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // 씬 전환
        fade.Fadeload("Yeonggyo_test");
    }

    IEnumerator WaitForSkip()
    {
        // ESC 키가 눌릴 때까지 대기
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
                Debug.Log("트루 전환");
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
