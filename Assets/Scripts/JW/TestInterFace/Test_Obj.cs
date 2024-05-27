using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Obj : MonoBehaviour, Test_InterFace
{
    public IEnumerator InterAction()
    {
        yield return null;
    }
}
