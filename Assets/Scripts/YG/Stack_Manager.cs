using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stack_Manager : MonoBehaviour
{
    public short drug = 0;
    public short dep_A = 0;
    public short love = 0;
    public short dep_B = 0;
    
    public bool Is_drug = false;
    public bool Is_dep_A = false;
    public bool Is_love = false;
    public bool Is_dep_B = false;

    public bool check_map = false;

    //SceneManager.LoadScene("gameover");

    public GameObject ending;
    Ending_manager end;
    public void AddDrug()
    {
        drug += 1;
        if (drug >= 2)
            Is_drug = true;
    }
    public void AddDep_A()
    {
        dep_A += 1;
        if (dep_A >= 2)
            Is_dep_A = true;
    }
    public void Addlove()
    {
        love += 1;
        if (love >= 2)
            Is_love = true;
    }
    public void AddDep_B()
    {
        dep_B += 1;
        if (dep_B >= 2)
            Is_dep_B = true;
    }
    public void subDep_A()
    {
        dep_A -= 1;
        if (dep_A < 2)
            Is_dep_A = false;
    }
    public void checkmap()
    {
        check_map = true;
    }

    void Start()
    {
        end = ending.GetComponent<Ending_manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
