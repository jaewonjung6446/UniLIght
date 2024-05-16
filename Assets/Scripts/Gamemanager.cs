using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject MenuPanel;
    private void Start()
    {
        MenuPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            MenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void Menu_Quit()
    {
        Application.Quit();
    }
    public void Menu_MainMenu()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Menu_Continue()
    {
        Time.timeScale = 1.0f;
        MenuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
