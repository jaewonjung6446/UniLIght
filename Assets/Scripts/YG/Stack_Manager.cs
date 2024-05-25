using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stack_Manager : MonoBehaviour
{
    public short drug = 0;
    public short dep = 0;
    public bool Is_drug = false;
    public bool Is_dep = false;

    //SceneManager.LoadScene("gameover");

    public GameObject ending;
    Ending_manager end;
    public void AddDrug()
    {
        drug += 1;
        if (drug >= 2)
            Is_drug = true;
    }
    public void AddDep()
    {
        dep += 1;
        if (dep >= 2)
            Is_dep = true;
    }
    public void subDep()
    {
        dep -= 1;
    }


    void Start()
    {
        end = ending.GetComponent<Ending_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Is_drug)
            end.ending = Ending_manager.Ending.drug_A;
        if (Is_dep)
            end.ending = Ending_manager.Ending.depressive_A;
    }
}
