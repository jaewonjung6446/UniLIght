using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 6f; //인식할 수 있는 범위
    public Text pauseText; // Inspector에서 할당
    [TextArea]
    public string[] Obj_Cube; // 표시할 메시지 배열
    [SerializeField] GameObject Sphere;
    GameObject hitObject;
    private int currentIndex = 0; // 현재 메시지 인덱스
    private bool toggleText = false;
    private bool interAction = false;
    private bool lerpPOV = false;
    public float moveSpeed = 1.0f;   // 이동 속도
    public float rotateSpeed = 1.0f; // 회전 속도
    private Vector3 startPosition;   // 시작 위치
    private Quaternion startRotation;// 시작 회전
    private Vector3 endPosition;     // 종료 위치
    private Quaternion endRotation;  // 종료 회전
    private float journeyLength;     // 총 이동 거리
    private float startTime;         // 시작 시간
    private bool movEnd = false;
    RaycastHit hit;
    Ray ray;
    [SerializeField] private Camera mainCam;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기
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
    //텍스트 순차 출력 메소드, message안에 출력할 텍스트 배열을 입력하면 추적해서 Update에서 출력함
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
                        Debug.Log("작동 중");
                        Time.timeScale = 0f; // 게임 일시 정지
                        pauseText.text = Obj_Cube[currentIndex]; // 현재 인덱스의 메시지 표시
                        pauseText.gameObject.SetActive(true); // 텍스트 활성화
                        currentIndex++; // 다음 메시지로 인덱스 증가
                    }
                    else if (Time.timeScale == 0 && currentIndex == Obj_Cube.Length)
                    {
                        Time.timeScale = 1.0f; // 게임 재개
                        pauseText.gameObject.SetActive(false); // 텍스트 숨기기
                        toggleText = false;
                    }
                    break;
            }
        }
    }
    GameObject GetInfo()
    {
        if (Input.GetKeyDown(KeyCode.E)) //키보드 E를 눌렀을 때
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //인식할 수 있는 범위 안에서 물체 확인
            {
                hitObject = hit.collider.gameObject; //주변 물체의 정보를 가져옵니다. 상호작용 중임을 나타내는 bool을 true로 합니다.
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
                Debug.Log("물체감지");
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = new Vector3(-2.01799989f, 0.996999979f, 0.468981743f);       // 목표 Transform의 위치를 종료 위치로 설정
                    endRotation = GetInfo().transform.rotation;       // 목표 Transform의 회전을 종료 회전으로 설정
                }
                journeyLength = Vector3.Distance(startPosition, endPosition); // 시작점과 종점 사이의 거리 계산
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
        // 보간 중의 거리를 로그로 출력
        Debug.Log((transform.position - endPosition).magnitude);

        if ((transform.position - endPosition).magnitude > 0.1f)
        {
            // 이동 중
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
        }
        if ((transform.position - endPosition).magnitude <= 0.1f && Input.GetKeyDown(KeyCode.E))
        {

            // 원상복구
            Debug.Log("원상복구");
            GetComponentInChildren<CameraSettings>().enabled = true;
            transform.position = startPosition;
            transform.rotation = startRotation;
            UnlockCursor();
            lerpPOV = false;
        }
    }

}