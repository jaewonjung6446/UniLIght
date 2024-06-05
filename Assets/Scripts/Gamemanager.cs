using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject MenuPanel;
    public static Gamemanager Instance;
    public bool StopAvilable = true;
    private Fade fade;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(MenuPanel);
        MenuPanel.SetActive(false);
        fade = FindObjectOfType<Fade>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && StopAvilable && SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "StartScene" && SceneManager.GetActiveScene().name != "EndingScene")
        {
            Time.timeScale = 0;
            MenuPanel.SetActive(true);
            CursorManager.Instance.UnlockCursor();
        }
    }
    public void Menu_Quit()
    {
        Application.Quit();
    }
    public void Menu_MainMenu()
    {
        Time.timeScale = 1.0f;
        MenuPanel.SetActive(false);
        fade.Fadeload("StartScene");
    }
    public void Menu_Continue()
    {
        Time.timeScale = 1.0f;
        MenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
