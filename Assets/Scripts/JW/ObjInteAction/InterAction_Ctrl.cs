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
    public float raycastDistance = 10f; //�ν��� �� �ִ� ����
    public Text pauseText; // Inspector���� �Ҵ�
    public Text desCription;
    [TextArea]
    public string[] Obj_Cube; // ǥ���� �޽��� �迭
    [TextArea]
    public string[] Picture;
    [TextArea]
    public string[] antidepressant;
    [TextArea]
    public string[] Radio; // ǥ���� �޽��� �迭
    public AudioSource audioSource;
    [SerializeField] private Image DesImage;
    private string[] printStrings = null;
    //[SerializeField] GameObject Sphere;
    [SerializeField] GameObject TextPanel;
    public bool DesTextAvailable = true;
    public GameObject hitObject;
    private int currentIndex = 0; // ���� �޽��� �ε���
    private bool toggleText = false;
    private bool interAction = false;
    private bool lerpPOV = false;
    private bool keyDown = false;
    public float moveSpeed = 1.0f;   // �̵� �ӵ�
    public float rotateSpeed = 1.0f; // ȸ�� �ӵ�
    private Vector3 startPosition;   // ���� ��ġ
    private Quaternion startRotation;// ���� ȸ��
    private Vector3 endPosition;     // ���� ��ġ
    private Quaternion endRotation;  // ���� ȸ��
    private float journeyLength;     // �� �̵� �Ÿ�
    private float startTime;         // ���� �ð�
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

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��
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
       //���� className�� ���� Ŭ������ �ҷ�����, ���߿���methodName�� ���� '�Ϲ� �޼ҵ�'�� �ҷ��ͼ� �ڵ����� �����ϴ� �Լ�.
    void CreateAndCallMethod(string className, string methodName)
    {
        // Ŭ���� Ÿ���� ���ڿ��� �˻�
        Type type = Type.GetType(className);
        if (type != null)
        {
            // �ش� Ÿ���� ������Ʈ�� ���� ������Ʈ�� �˻�
            Component component = FindObjectOfType(type) as Component;

            if (component != null)
            {
                // �޼��带 ȣ��
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
    //������ className�� ���� Ŭ������ �ҷ�����, ���߿���methodName�� ���� '�ڷ�ƾ'�� �ҷ��ͼ� �ڵ����� �����ϴ� �Լ�.
    void CreateAndCallCoroutine(string className, string coroutineName)
    {
        // Ŭ���� Ÿ���� ���ڿ��� �˻�
        Type type = Type.GetType(className);
        if (type != null)
        {
            // ���� ������Ʈ�� �����ϰ� �ش� Ÿ���� ������Ʈ�� �߰�
            GameObject obj = new GameObject(className);
            MonoBehaviour component = obj.AddComponent(type) as MonoBehaviour;

            // �ڷ�ƾ �޼��带 ȣ��
            MethodInfo method = type.GetMethod(coroutineName);
            if (method != null)
            {
                // �ڷ�ƾ ����
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
        // StartCoroutine�� ȣ���ϱ� ���� ���� �޼��带 ���
        StartCoroutine(CoroutineStarter(component, method));
    }

    IEnumerator CoroutineStarter(MonoBehaviour component, MethodInfo method)
    {
        // �ڷ�ƾ�� ȣ���ϰ� IEnumerator ��ȯ
        yield return (IEnumerator)method.Invoke(component, null);
    }
    //���� �� �޼ҵ��  className �� �Է��ϸ� ���� �̸��� ������Ʈ�� className�� �̸��� ����
    //��ũ��Ʈ�� �˻��ؼ� (�ݵ�� ��ũ��Ʈ ���� ������Ʈ �̸��� ���ƾ���) ��ũ��Ʈ�� ��ӹ��� InterFace���� InterAction()�� �ڵ����� ȣ����.
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
            Debug.Log("�ν�");
            CreateAndCallCoroutine(GetInfo().name, "InterAction");
        }
        else
        {
            Debug.Log("�ν� ��");
        }
    }
}