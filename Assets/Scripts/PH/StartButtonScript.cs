using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    private Fade fade;

    public void Start()
    {
        fade = FindObjectOfType<Fade>();
    }
    public void StartGame()
    {
        fade.Fadeload("Tutorial");
    }
}