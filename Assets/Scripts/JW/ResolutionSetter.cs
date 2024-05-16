using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetter : MonoBehaviour
{
    void Start()
    {
        // ���� ��ũ�� �ػ󵵸� �����ɴϴ�.
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        // Canvas Scaler ������Ʈ�� ã���ϴ�.
        CanvasScaler scaler = GetComponent<CanvasScaler>();

        if (scaler != null)
        {
            // Canvas Scaler�� Reference Resolution�� ���� �ػ󵵷� �����մϴ�.
            scaler.referenceResolution = currentResolution;
        }
    }
}
