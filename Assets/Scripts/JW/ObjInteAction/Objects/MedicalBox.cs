using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalBox : MonoBehaviour,Obj_Interface
{
    [SerializeField] private GameObject medicalBox;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E) && !medicalBox.activeSelf)
        {
            Time.timeScale = 0;
            medicalBox.SetActive(true);
        }else if (Input.GetKeyDown(KeyCode.E) && medicalBox.activeSelf)
        {
            Time.timeScale = 1;
            medicalBox.SetActive(false);
        }
    }
}
