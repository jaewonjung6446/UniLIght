using UnityEngine;
using System.Collections;

public class GlobalCoroutineRunner : MonoBehaviour
{
    private static GlobalCoroutineRunner instance;

    public static GlobalCoroutineRunner Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GlobalCoroutineRunner");
                instance = go.AddComponent<GlobalCoroutineRunner>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public static void StartGlobalCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(coroutine);
    }
}
