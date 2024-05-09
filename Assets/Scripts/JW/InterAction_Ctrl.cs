using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 10f; //인식할 수 있는 범위
    public Text pauseText; // Inspector에서 할당
    public Text desCription;
    [TextArea]
    public string[] Obj_Cube; // 표시할 메시지 배열
    [TextArea]
    public string[] Picture;

    private string[] printStrings = null;
    [SerializeField] GameObject Sphere;
    GameObject hitObject;
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
    //private bool movEnd = false;
    private Coroutine toggleTextCoroutine = null;
    private Coroutine turnPOVCoroutine = null;
    RaycastHit hit;
    Ray ray;
    [SerializeField] private Camera mainCam;
    void Update()
    {

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기
        if(Physics.Raycast(ray,out hit, raycastDistance)){
            hitObject = hit.collider.gameObject;
            if(hitObject != null)
            {
                desCription.gameObject.SetActive(true);
                desCription.text = hitObject.name;
            }
        }
        else
        {
            desCription.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            GetInfo();
        }
        DoWhat();
        if (lerpPOV && turnPOVCoroutine == null) turnPOVCoroutine = StartCoroutine(TurnPOV());
        if (!lerpPOV)
        {
            turnPOVCoroutine = null;
            StopCoroutine(TurnPOV());
        }

        if (toggleText && turnPOVCoroutine == null) toggleTextCoroutine = StartCoroutine(ToggleText()); // 코루틴 시작
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
                    Debug.Log("실행 시작");
                }
        return hitObject;
    }
    private void DoWhat()
    {
        if (hitObject != null && interAction)
        {
            if (hitObject.name == "Obj_Cube")
            {
                toggleText = true;
                printStrings = Obj_Cube;
            }
            if (hitObject.name == "Picture")
            {
                toggleText = true;
                printStrings = Picture;
            }
            if (hitObject.name == "Flag")
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
                lerpPOV = true;
            }
            if (hitObject.name == "Flag2")
            {
                Debug.Log("물체감지2");
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = new Vector3(0.340000004f, 0.996999979f, -0.949999988f);      // 목표 Transform의 위치를 종료 위치로 설정
                    endRotation = GetInfo().transform.rotation;       // 목표 Transform의 회전을 종료 회전으로 설정
                }
                journeyLength = Vector3.Distance(startPosition, endPosition); // 시작점과 종점 사이의 거리 계산
                startTime = Time.time;
                GetComponentInChildren<CameraSettings>().enabled = false;
                lerpPOV = true;
            }
        }
        interAction = false;
    }

    IEnumerator TurnPOV()
    {
        Debug.Log("이동시작");
        while (true)
        {
            if ((transform.position - endPosition).magnitude > 0.1f)
            {
                if ((transform.position - endPosition).magnitude <= 0.3f && hitObject.name == "Flag2")
                {
                    SceneManager.LoadScene("SceneTranformTest");
                    break;
                }
                // 이동 중
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
            }

            if ((transform.position - endPosition).magnitude <= 0.1f && Input.GetKeyDown(KeyCode.E))
            {
                GetComponentInChildren<CameraSettings>().enabled = true;
                this.transform.position = startPosition;
                Debug.Log("원상복구" + startPosition + "/" + this.transform.position);
                lerpPOV = false;
                Debug.Log("이동 끝");
                break;
            }
            yield return null;
        }
    }
    IEnumerator ToggleText()
    {
        desCription.gameObject.SetActive(false);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E키 입력");
                if (currentIndex < printStrings.Length)
                {
                    Time.timeScale = 0; // 게임 일시 정지
                    pauseText.text = printStrings[currentIndex]; // 현재 인덱스의 메시지 표시
                    pauseText.gameObject.SetActive(true); // 텍스트 활성화
                    currentIndex++; // 다음 메시지로 인덱스 증가
                }
                else
                {
                    Time.timeScale = 1.0f; // 게임 재개
                    pauseText.gameObject.SetActive(false); // 텍스트 숨기기
                    pauseText.text = null;
                    toggleText = false;
                    currentIndex = 0;
                    if (hitObject.GetComponent<InventoryManager>() != null) hitObject.GetComponent<InventoryManager>().AddToInventory(hitObject);
                    break;
                }
            }
        yield return new WaitForFixedUpdate();
        }
    }
}