using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Ending_manager;

public class ending_test : MonoBehaviour, Obj_Interface
{
    private Fade fade;
    public void Start()
    {
        fade = FindObjectOfType<Fade>();
    }
    
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            fade.Fadeload("EndingScene");
        }
    }
}