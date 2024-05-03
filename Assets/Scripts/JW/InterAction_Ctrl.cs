using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 6f; //�ν��� �� �ִ� ����
    public Text pauseText; // Inspector���� �Ҵ�
    [TextArea]
    public string[] Obj_Cube; // ǥ���� �޽��� �迭
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
    RaycastHit hit;
    Ray ray;
    [SerializeField] private Camera mainCam;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��
        GetInfo();
        DoWhat();
        if (toggleText)
        {
            //Debug.Log(currentIndex.ToString());
            ToggleText(GetInfo());
        }
        if (lerpPOV)
        {
            TurnPOV();
        }
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //�ؽ�Ʈ ���� ��� �޼ҵ�, message�ȿ� ����� �ؽ�Ʈ �迭�� �Է��ϸ� �����ؼ� Update���� �����
    void ToggleText(GameObject interActionObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (interActionObj.name)
            {
                case "Obj_Cube":
                    Debug.Log(currentIndex < Obj_Cube.Length);
                    if (currentIndex < Obj_Cube.Length)
                    {
                        Debug.Log("�۵� ��");
                        Time.timeScale = 0f; // ���� �Ͻ� ����
                        pauseText.text = Obj_Cube[currentIndex]; // ���� �ε����� �޽��� ǥ��
                        pauseText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
                        currentIndex++; // ���� �޽����� �ε��� ����
                    }
                    else if (Time.timeScale == 0 && currentIndex == Obj_Cube.Length)
                    {
                        Time.timeScale = 1.0f; // ���� �簳
                        pauseText.gameObject.SetActive(false); // �ؽ�Ʈ �����
                        toggleText = false;
                    }
                    break;
            }
        }
    }
    GameObject GetInfo()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Ű���� E�� ������ ��
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //�ν��� �� �ִ� ���� �ȿ��� ��ü Ȯ��
            {
                hitObject = hit.collider.gameObject; //�ֺ� ��ü�� ������ �����ɴϴ�. ��ȣ�ۿ� ������ ��Ÿ���� bool�� true�� �մϴ�.
                if(hitObject != null)
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
            if (hitObject.GetComponent<InventoryManager>() != null)
            {
                hitObject.GetComponent<InventoryManager>().AddToInventory(hitObject);
                toggleText = true;
            }
            if (hitObject.CompareTag("Flag"))
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
                LockCursor();
                lerpPOV = true;
            }
        }
        interAction = false;
    }

    private void TurnPOV()
    {
        // ���� ���� �Ÿ��� �α׷� ���
        Debug.Log((transform.position - endPosition).magnitude);

        if ((transform.position - endPosition).magnitude > 0.1f)
        {
            // �̵� ��
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
        }
        if ((transform.position - endPosition).magnitude <= 0.1f && Input.GetKeyDown(KeyCode.E))
        {

            // ���󺹱�
            Debug.Log("���󺹱�");
            GetComponentInChildren<CameraSettings>().enabled = true;
            transform.position = startPosition;
            transform.rotation = startRotation;
            UnlockCursor();
            lerpPOV = false;
        }
    }

}