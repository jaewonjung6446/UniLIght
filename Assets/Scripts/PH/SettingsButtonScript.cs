using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButtonScript : MonoBehaviour
{
    public GameObject settingsPanel;

    public void ToggleSettingsPanel()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
        Time.timeScale = isActive ? 1 : 0;
    }
}