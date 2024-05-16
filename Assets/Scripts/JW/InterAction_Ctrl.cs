using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterAction_Ctrl : MonoBehaviour
{
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

    [SerializeField] private Image DesImage;
    private string[] printStrings = null;
    [SerializeField] GameObject Sphere;
    [SerializeField] GameObject TextPanel;

    GameObject hitObject;
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

    //private bool movEnd = false;
    private Coroutine toggleTextCoroutine = null;
    private Coroutine turnPOVCoroutine = null;
    RaycastHit hit;
    Ray ray;
    [SerializeField] private Camera mainCam;
    private void Start()
    {
        TextPanel.SetActive(false);
    }
    void Update()
    {

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��
        #region Input�Ŵ���
        if (Input.GetKeyDown(KeyCode.E)) pressE = true;
        else pressE = false;
        if (Input.GetKeyDown(KeyCode.Q)) pressQ = true;
        else pressQ = false;
        #endregion
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            hitObject = hit.collider.gameObject;
            if (hitObject != null)
            {
                desCription.gameObject.SetActive(true);
                desCription.text = hitObject.name;
            }
        }
        else
        {
            desCription.gameObject.SetActive(false);
        }
        if (pressE)
        {
            GetInfo();
        }
        DoWhat();
        if (toggleText && toggleTextCoroutine == null)
        {
            toggleTextCoroutine = StartCoroutine(ToggleText());
            Debug.Log("�ڷ�ƾ ����");
        }// �ڷ�ƾ ����
        if (lerpPOV && turnPOVCoroutine == null)
        {
            turnPOVCoroutine = StartCoroutine(TurnPOV());
        }
        if (!lerpPOV)
        {
            turnPOVCoroutine = null;
            StopCoroutine(TurnPOV());
        }
        if (!toggleText)
        {
            toggleTextCoroutine = null;
            StopCoroutine(ToggleText());
        }
    }
    GameObject GetInfo()
    {
        desCription.gameObject.SetActive(false);
        if (hitObject != null)
        {
            interAction = true;
        }
        return hitObject;
    }
    private void DoWhat()
    {
        if (hitObject != null && interAction)
        {
            if (hitObject.name == "�˵� ���� ������")
            {
                toggleText = true;
                printStrings = Obj_Cube;
            }
            if (hitObject.name == "����")
            {
                toggleText = true;
                printStrings = Picture;
                DesImage.gameObject.SetActive(true);
                if (Resources.Load<Sprite>("Images/����") != null)
                {
                    DesImage.sprite = Resources.Load<Sprite>("Images/����");
                }

            }
            if (hitObject.name == "�׿����")
            {
                toggleText = true;
                printStrings = null;
            }
            if (hitObject.name == "����")
            {
                toggleText = true;
                printStrings = Radio;
            }

            if (hitObject.name == "����")
            {
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = hitObject.transform.position;       // ��ǥ Transform�� ��ġ�� ���� ��ġ�� ����
                    endRotation = GetInfo().transform.rotation;       // ��ǥ Transform�� ȸ���� ���� ȸ������ ����
                }
                journeyLength = Vector3.Distance(startPosition, endPosition); // �������� ���� ������ �Ÿ� ���
                startTime = Time.time;
                GetComponentInChildren<CameraSettings>().enabled = false;
                lerpPOV = true;
            }
            if (hitObject.name == "����ȯ�׽�Ʈ")
            {
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = hitObject.transform.position;       // ��ǥ Transform�� ��ġ�� ���� ��ġ�� ����
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
        Debug.Log("�̵�����");
        while (true)
        {
            raycastDistance = 0;
            if ((transform.position - endPosition).magnitude > 0.1f)
            {
                //����ȯ �׽�Ʈ
                if ((transform.position - endPosition).magnitude <= 0.3f && hitObject.name == "����ȯ�׽�Ʈ" && SceneManager.GetActiveScene().name == "Jaewon_Test1")
                {
                    SceneManager.LoadScene("Jaewon_Test2");
                    break;
                }
                else if ((transform.position - endPosition).magnitude <= 0.3f && hitObject.name == "����ȯ�׽�Ʈ" && SceneManager.GetActiveScene().name == "Jaewon_Test2")
                {
                    SceneManager.LoadScene("Jaewon_Test1");
                }

                // �̵� ��
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
            }

            if ((transform.position - endPosition).magnitude <= 0.1f && pressE)
            {
                GetComponentInChildren<CameraSettings>().enabled = true;
                this.transform.position = startPosition;
                Debug.Log("���󺹱�" + startPosition + "/" + this.transform.position);
                lerpPOV = false;
                Debug.Log("�̵� ��");
                raycastDistance = 10;
                break;
            }
            yield return null;
        }
    }
    IEnumerator ToggleText()
    {
        raycastDistance = 0;
        desCription.gameObject.SetActive(false);
        TextPanel.SetActive(true);
        #region �������
        if (hitObject.name == "�׿����")
        {
            Debug.Log("�׿���� ����");
            Time.timeScale = 0f; // ���� �Ͻ� ����
            pauseText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
            DesImage.gameObject.SetActive(true);
            if (Resources.Load<Sprite>("Images/antidepressant") != null)
            {
                DesImage.sprite = Resources.Load<Sprite>("Images/antidepressant");
                pauseText.text = "���������� �ƹ� ������ ���� �ʰ� �ȴ�.\n���� �Ͻðڽ��ϱ�? \n (E�� ���� ����/Q�� ���)";
            }
            else
            {
                Debug.Log("����� �̹��� �ε����");
            }

            // Ű �Է��� ��ٸ��� ����
            bool done = false;

            while (!done)
            {
                yield return null; // ���� �����ӱ��� ��ٸ�
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("����");
                    if (Resources.Load<Sprite>("Images/swallow") != null)
                    {
                        DesImage.sprite = Resources.Load<Sprite>("Images/swallow");
                    }
                    else
                    {
                        Debug.Log("�̹��� �ε����");
                    }
                    pauseText.text = "������ ������ �ȴ�";
                    bool done2 = false;
                    while (!done2)
                    {
                        yield return new WaitForSecondsRealtime(1.5f);
                        toggleText = false;
                        done2 = true;
                    }
                    done = true; // ���� ����
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("�������");
                    toggleText = false;
                    done = true; // ���� ����
                }
            }

            // ���� �簳
            Time.timeScale = 1.0f;
            TextPanel.SetActive(false);
            pauseText.gameObject.SetActive(false);
            DesImage.gameObject.SetActive(false);
            raycastDistance = 10;
        }
        #endregion
        #region ����Ʈ ��¹��
        while (printStrings != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("EŰ �Է�");
                if (currentIndex < printStrings.Length)
                {
                    Time.timeScale = 0; // ���� �Ͻ� ����
                    pauseText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
                    pauseText.text = printStrings[currentIndex]; // ���� �ε����� �޽��� ǥ��
                    currentIndex++; // ���� �޽����� �ε��� ����
                }
                else
                {
                    Time.timeScale = 1.0f; // ���� �簳
                    pauseText.gameObject.SetActive(false); // �ؽ�Ʈ �����
                    pauseText.text = null;
                    toggleText = false;
                    currentIndex = 0;
                    raycastDistance = 10;
                    TextPanel.SetActive(false);
                    DesImage.gameObject.SetActive(false);
                    hitObject = null;
                    break;
                }
            }
            yield return null;
        }
        #endregion
    }
}