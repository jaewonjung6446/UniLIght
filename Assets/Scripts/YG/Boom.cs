using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Fade fade;
    private Ending_manager end;
    public void Start()
    {
        fade = FindObjectOfType<Fade>();
        end = FindObjectOfType<Ending_manager>();
    }

    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            end.ending = Ending_manager.Ending.final_1;
            fade.Fadeload("EndingScene");
        }
    }
}
