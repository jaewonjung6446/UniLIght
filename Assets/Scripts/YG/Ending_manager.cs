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
        None,           //엔딩조건 없음
        depressive_A,   //엔딩1
        drug_A,         //엔딩2
        depressive_B,   //엔딩3
        love_B,         //엔딩4
        boom_fail,      //엔딩5
        heal_fail,      //엔딩6
        final_1,        //엔딩7
        final_2         //엔딩8
    }

    void Start()
    {
        ending = Ending.None;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
