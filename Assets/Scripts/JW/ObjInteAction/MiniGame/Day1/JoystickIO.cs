using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickIO : MonoBehaviour
{
    [SerializeField] Gamemanager gamemanager;
    void Start()
    {
        Debug.Log("미니게임 시작");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            CursorManager.Instance.LockCursor();
            gamemanager.StopAvilable = true;
        }
        if (this.gameObject.activeSelf){
            Time.timeScale = 0;
            gamemanager.StopAvilable = false;
        }
    }
}
