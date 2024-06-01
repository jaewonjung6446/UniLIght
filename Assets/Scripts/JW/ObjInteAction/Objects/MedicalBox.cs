using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalBox : MonoBehaviour,Obj_Interface
{
    [SerializeField] private GameObject medicalBox;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            medicalBox.SetActive(true);
        }
    }
}
