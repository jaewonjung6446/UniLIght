using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSetter : MonoBehaviour
{
    void Start()
    {
        // 현재 스크린 해상도를 가져옵니다.
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        // Canvas Scaler 컴포넌트를 찾습니다.
        CanvasScaler scaler = GetComponent<CanvasScaler>();

        if (scaler != null)
        {
            // Canvas Scaler의 Reference Resolution을 현재 해상도로 설정합니다.
            scaler.referenceResolution = currentResolution;
        }
    }
}
