using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 10f; //인식할 수 있는 범위
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
        if (lerpPOV) StartCoroutine(TurnPOV());
        if (toggleText) StartCoroutine(ToggleText()); // 코루틴 시작
    }
    GameObject GetInfo()
    {
        if (Input.GetKeyDown(KeyCode.E)) //키보드 E를 눌렀을 때
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //인식할 수 있는 범위 안에서 물체 확인
            {
                hitObject = hit.collider.gameObject; //주변 물체의 정보를 가져옵니다. 상호작용 중임을 나타내는 bool을 true로 합니다.
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
                
                if(hitObject.GetComponent<InventoryManager>() !=null) hitObject.GetComponent<InventoryManager>().AddToInventory(hitObject);
                toggleText = true;
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
        Debug.Log((transform.position - endPosition).magnitude);
        if((transform.position - endPosition).magnitude <= 0.3f && hitObject.name == "Flag2")
        {
            SceneManager.LoadScene("SceneTranformTest");
        }
        if ((transform.position - endPosition).magnitude > 0.1f)
        {
            // 이동 중
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
            yield return null;
        }
        if ((transform.position - endPosition).magnitude <= 0.1f && Input.GetKeyDown(KeyCode.E))
        {

            // 원상복구
            Debug.Log("원상복구");
            GetComponentInChildren<CameraSettings>().enabled = true;
            transform.position = startPosition;
            transform.rotation = startRotation;
            lerpPOV = false;
            yield return null;
        }
    }
    IEnumerator ToggleText()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentIndex < Obj_Cube.Length)
            {
                Time.timeScale = 0f; // 게임 일시 정지
                pauseText.text = Obj_Cube[currentIndex]; // 현재 인덱스의 메시지 표시
                pauseText.gameObject.SetActive(true); // 텍스트 활성화
                currentIndex++; // 다음 메시지로 인덱스 증가
                Debug.Log("E키 입력 대기");

                //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E)); // 사용자가 다시 E를 누를 때까지 대기
                yield return null;
            }
            else
            {
                Time.timeScale = 1.0f; // 게임 재개
                pauseText.gameObject.SetActive(false); // 텍스트 숨기기
                toggleText = false;
                yield return null;
            }
        }
    }
}