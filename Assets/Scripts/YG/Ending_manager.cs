using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_manager : MonoBehaviour
{

    public Ending ending;
    public enum Ending
    {
        None,
        drug_A,
        depressive_A,
        depressive_B,
        normal_B,
        final_1,
        final_2
    }

    void Start()
    {
        ending = Ending.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (ending != Ending.None)
        {
            if (ending == Ending.drug_A)
                Drug_A();
            else if (ending == Ending.depressive_A)
                Depressive_A();
            else if (ending == Ending.depressive_B)
                Depressive_B();
            else if (ending == Ending.normal_B)
                Normal_B();
            else if (ending == Ending.final_1)
                Final_1();
            else if (ending == Ending.final_2)
                Final_2();
        }
    }

    private void Final_2()
    {
        throw new NotImplementedException();
    }

    private void Final_1()
    {
        throw new NotImplementedException();
    }

    private void Normal_B()
    {
        throw new NotImplementedException();
    }

    private void Depressive_B()
    {
        throw new NotImplementedException();
    }

    private void Depressive_A()
    {
        throw new NotImplementedException();
    }

    private void Drug_A()
    {
        SceneManager.LoadScene("drug_A");
    }
}
