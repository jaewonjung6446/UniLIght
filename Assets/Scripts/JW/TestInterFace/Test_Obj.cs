using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Obj : MonoBehaviour, Test_InterFace
{
    public IEnumerator InterAction()
    {
        Debug.Log("테스트용 코루틴 인터페이스");
        yield return null;
    }
}
