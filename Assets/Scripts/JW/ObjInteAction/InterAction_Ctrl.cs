using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;


public class InterAction_Ctrl : MonoBehaviour
{
    public static InterAction_Ctrl Instance;
    public float raycastDistance = 10f; //인식할 수 있는 범위
    public Text pauseText; // Inspector에서 할당
    public Text desCription;
    [TextArea]
    public string[] Obj_Cube; // 표시할 메시지 배열
    [TextArea]
    public string[] Picture;
    [TextArea]
    public string[] antidepressant;
    [TextArea]
    public string[] Radio; // 표시할 메시지 배열
    public AudioSource audioSource;
    [SerializeField] private Image DesImage;
    private string[] printStrings = null;
    //[SerializeField] GameObject Sphere;
    [SerializeField] GameObject TextPanel;
    public bool DesTextAvailable = true;
    public GameObject hitObject;
    private int currentIndex = 0; // 현재 메시지 인덱스
    private bool toggleText = false;
    private bool interAction = false;
    private bool lerpPOV = false;
    private bool keyDown = false;
    public float moveSpeed = 1.0f;   // 이동 속도
    public float rotateSpeed = 1.0f; // 회전 속도
    private Vector3 startPosition;   // 시작 위치
    private Quaternion startRotation;// 시작 회전
    private Vector3 endPosition;     // 종료 위치
    private Quaternion endRotation;  // 종료 회전
    private float journeyLength;     // 총 이동 거리
    private float startTime;         // 시작 시간
    private bool pressE = false;
    private bool pressQ = false;
    Tutorial tutorial;

    //private bool movEnd = false;
    private Coroutine toggleTextCoroutine = null;
    private Coroutine turnPOVCoroutine = null;
    RaycastHit hit;
    Ray ray;
    [SerializeField] private Camera mainCam;
    private void Start()
    {
        TextPanel.SetActive(false);
        Instance = this;
        tutorial = FindObjectOfType<Tutorial>();
        desCription.gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            hitObject = hit.collider.gameObject;
        }
        else
        {
            hitObject = null;
        }
        DesText();
        if (GetInfo() != null)
        {
            //Debug.Log(hitObject.name);
            GetMethodAndRun(hitObject.name);
        }
    }
    void DesText()
    {
        if (DesTextAvailable)
        {
            if (hitObject != null)
            {
                desCription.text = hitObject.name;
            }
            else
            {
                desCription.text = "";
            }
        }
        else
        {
            desCription.text = "";
        }
    }
    public GameObject GetInfo()
    {
        if (hitObject != null)
        {
            return hitObject;
        }
        else { return null; }
    }
       //인자 className과 같은 클래스를 불러오고, 그중에서methodName과 같은 '일반 메소드'를 불러와서 자동으로 실행하는 함수.
    void CreateAndCallMethod(string className, string methodName)
    {
        // 클래스 타입을 문자열로 검색
        Type type = Type.GetType(className);
        if (type != null)
        {
            // 해당 타입의 컴포넌트를 가진 오브젝트를 검색
            Component component = FindObjectOfType(type) as Component;

            if (component != null)
            {
                // 메서드를 호출
                MethodInfo method = type.GetMethod(methodName);
                if (method != null)
                {
                    method.Invoke(component, null);
                }
                else
                {
                    //Debug.LogError($"Method {methodName} not found in {className}.");
                }
            }
            else
            {
                //Debug.LogError($"Component of type {className} not found in the scene.");
            }
        }
        else
        {
            //Debug.LogError($"Class {className} not found.");
        }
    }
    //위인자 className과 같은 클래스를 불러오고, 그중에서methodName과 같은 '코루틴'을 불러와서 자동으로 실행하는 함수.
    void CreateAndCallCoroutine(string className, string coroutineName)
    {
        // 클래스 타입을 문자열로 검색
        Type type = Type.GetType(className);
        if (type != null)
        {
            // 게임 오브젝트를 생성하고 해당 타입의 컴포넌트를 추가
            GameObject obj = new GameObject(className);
            MonoBehaviour component = obj.AddComponent(type) as MonoBehaviour;

            // 코루틴 메서드를 호출
            MethodInfo method = type.GetMethod(coroutineName);
            if (method != null)
            {
                // 코루틴 시작
                StartCoroutineWrapper(component, method);
            }
            else
            {
                Debug.LogError($"Coroutine {coroutineName} not found in {className}.");
            }
        }
        else
        {
            Debug.LogError($"Class {className} not found.");
        }
    }
    void StartCoroutineWrapper(MonoBehaviour component, MethodInfo method)
    {
        // StartCoroutine을 호출하기 위해 래퍼 메서드를 사용
        StartCoroutine(CoroutineStarter(component, method));
    }

    IEnumerator CoroutineStarter(MonoBehaviour component, MethodInfo method)
    {
        // 코루틴을 호출하고 IEnumerator 반환
        yield return (IEnumerator)method.Invoke(component, null);
    }
    //밑의 두 메소드는  className 만 입력하면 같은 이름의 오브젝트의 className과 이름이 같은
    //스크립트를 검색해서 (반드시 스크립트 명은 오브젝트 이름과 같아야함) 스크립트가 상속받은 InterFace내의 InterAction()를 자동으로 호출함.
    void GetMethodAndRun(string className)
    {
        if (GetInfo() != null && GetInfo().name == className)
        {
            //Type type = Type.GetType(GetInfo().name);
            CreateAndCallMethod(GetInfo().name, "InterAction");
        }
    }

    void GetCoroutineAndRun(string className)
    {
        if (GetInfo() != null && GetInfo().name == className)
        {
            //Type type = Type.GetType(GetInfo().name);
            Debug.Log("인식");
            CreateAndCallCoroutine(GetInfo().name, "InterAction");
        }
        else
        {
            Debug.Log("인식 끝");
        }
    }
}