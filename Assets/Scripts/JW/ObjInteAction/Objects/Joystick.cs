using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour, Obj_Interface
{
    public GameObject uiPanel; // UI ÆÐ³Î
    void Awake()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (uiPanel != null)
            {
                Time.timeScale = 0.01f;
                uiPanel.SetActive(true);
            }
        }
    }
}
