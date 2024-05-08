using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 10f; //�ν��� �� �ִ� ����
    public Text pauseText; // Inspector���� �Ҵ�
    [TextArea]
    public string[] Obj_Cube; // ǥ���� �޽��� �迭
    private string[] printStrings = null;
    [SerializeField] GameObject Sphere;
    GameObject hitObject;
    private int currentIndex = 0; // ���� �޽��� �ε���
    private bool toggleText = false;
    private bool interAction = false;
    private bool lerpPOV = false;
    public float moveSpeed = 1.0f;   // �̵� �ӵ�
    public float rotateSpeed = 1.0f; // ȸ�� �ӵ�
    private Vector3 startPosition;   // ���� ��ġ
    private Quaternion startRotation;// ���� ȸ��
    private Vector3 endPosition;     // ���� ��ġ
    private Quaternion endRotation;  // ���� ȸ��
    private float journeyLength;     // �� �̵� �Ÿ�
    private float startTime;         // ���� �ð�
    private bool movEnd = false;
    private Coroutine toggleTextCoroutine = null;
    private Coroutine turnPOVCoroutine = null;
    RaycastHit hit;
    Ray ray;
    [SerializeField] private Camera mainCam;
    void Update()
    {

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��
        GetInfo();
        DoWhat();
        if (lerpPOV) StartCoroutine(TurnPOV());
        if (!lerpPOV) StopCoroutine(TurnPOV());

        if (toggleText) StartCoroutine(ToggleText()); // �ڷ�ƾ ����
        if (!toggleText) StopCoroutine(ToggleText()); // �ڷ�ƾ ����

    }
    GameObject GetInfo()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Ű���� E�� ������ ��
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //�ν��� �� �ִ� ���� �ȿ��� ��ü Ȯ��
            {
                hitObject = hit.collider.gameObject; //�ֺ� ��ü�� ������ �����ɴϴ�. ��ȣ�ۿ� ������ ��Ÿ���� bool�� true�� �մϴ�.
                if (hitObject != null)
                {
                    interAction = true;
                }
            }
        }
        return hitObject;
    }
    private void DoWhat()
    {
        if (hitObject != null && interAction)
        {
            if (hitObject.name == "Obj_Cube")
            {
                if (hitObject.GetComponent<InventoryManager>() !=null) hitObject.GetComponent<InventoryManager>().AddToInventory(hitObject);
                //printStrings = Obj_Cube;
                toggleText = true;
            }
            if (hitObject.name == "Flag")
            {
                Debug.Log("��ü����");
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = new Vector3(-2.01799989f, 0.996999979f, 0.468981743f);       // ��ǥ Transform�� ��ġ�� ���� ��ġ�� ����
                    endRotation = GetInfo().transform.rotation;       // ��ǥ Transform�� ȸ���� ���� ȸ������ ����
                }
                journeyLength = Vector3.Distance(startPosition, endPosition); // �������� ���� ������ �Ÿ� ���
                startTime = Time.time;
                GetComponentInChildren<CameraSettings>().enabled = false;
                lerpPOV = true;
            }
            if (hitObject.name == "Flag2")
            {
                Debug.Log("��ü����2");
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = new Vector3(0.340000004f, 0.996999979f, -0.949999988f);      // ��ǥ Transform�� ��ġ�� ���� ��ġ�� ����
                    endRotation = GetInfo().transform.rotation;       // ��ǥ Transform�� ȸ���� ���� ȸ������ ����
                }
                journeyLength = Vector3.Distance(startPosition, endPosition); // �������� ���� ������ �Ÿ� ���
                startTime = Time.time;
                GetComponentInChildren<CameraSettings>().enabled = false;
                lerpPOV = true;
            }
        }
        interAction = false;
    }

    IEnumerator TurnPOV()
    {
        Debug.Log("�̵� ����,"+startPosition);
        while (hitObject.name == "Flag2")
        {
            if ((transform.position - endPosition).magnitude <= 0.3f)
            {
                SceneManager.LoadScene("SceneTranformTest");
                break;
            }
        }
        while ((transform.position - endPosition).magnitude > 0.1f)
            {
                // �̵� ��
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
                yield return null;
            }
        
        if((transform.position - endPosition).magnitude <= 0.1f && Input.GetKeyDown(KeyCode.E))
        {
            GetComponentInChildren<CameraSettings>().enabled = true;
            this.transform.position = startPosition;
            Debug.Log("���󺹱�"+ startPosition + "/"+ this.transform.position);
            lerpPOV = false;
            Debug.Log("�̵� ��");
        }
    }
    IEnumerator ToggleText()
    {
        //������Ʈ �̸��� ���� string[]����
        switch (hitObject.name)
        {
            case "Obj_Cube":
                printStrings = Obj_Cube;
                break;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentIndex < printStrings.Length)
            {
                Time.timeScale = 0f; // ���� �Ͻ� ����
                pauseText.text = printStrings[currentIndex]; // ���� �ε����� �޽��� ǥ��
                pauseText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
                currentIndex++; // ���� �޽����� �ε��� ����
                Debug.Log("EŰ �Է� ���");
                yield return null;
            }
            else
            {
                Time.timeScale = 1.0f; // ���� �簳
                pauseText.gameObject.SetActive(false); // �ؽ�Ʈ �����
                toggleText = false;
                
                yield return null;
            }
        }
    }
}