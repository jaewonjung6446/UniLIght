using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Fade : MonoBehaviour
{
    private Image fadeImage;
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        GameObject par = GameObject.Find("fademanager");
        DontDestroyOnLoad(par);
        if (fadeImage == null)
        {
            fadeImage = GetComponent<Image>();
        }
    }

    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        StartCoroutine(FadeIn());
    }

    public void Fadeload(string sceneName)
    {
        StartCoroutine(FadeoutLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.enabled = false;
    }

    private IEnumerator FadeoutLoad(string sceneName)
    {
        fadeImage.enabled = true;
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        StartCoroutine(FadeIn());
    }
    
}