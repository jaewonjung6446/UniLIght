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

    public bool check_map = false;
    public bool send_msg = false;

    private Ending_manager end;
    public void AddDrug()
    {
        drug += 1;
        if (drug >= 2)
            end.ending = Ending_manager.Ending.drug_A;
    }
    public void AddDep_A()
    {
        dep_A += 1;
        if (dep_A >= 2)
            end.ending = Ending_manager.Ending.depressive_A;
    }
    public void Addlove()
    {
        love += 1;
        if (love >= 2)
            end.ending = Ending_manager.Ending.love_B;
    }
    public void AddDep_B()
    {
        dep_B += 1;
        if (dep_B >= 2)
            end.ending = Ending_manager.Ending.depressive_B;
    }
    public void subDep_A()
    {
        dep_A -= 1;
        if (dep_A < 2)
        {
            if (drug >= 2)
                end.ending = Ending_manager.Ending.drug_A;
            else if (dep_A >= 2)
                end.ending = Ending_manager.Ending.depressive_A;
            else if (love >= 2)
                end.ending = Ending_manager.Ending.love_B;
            else if (dep_B >= 2)
                end.ending = Ending_manager.Ending.depressive_B;
        }
    }
    public void CheckMap()
    {
        check_map = true;
    }
    public void SendMsg()
    {
        send_msg = true;
    }

    void Start()
    {
        end = FindObjectOfType<Ending_manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
