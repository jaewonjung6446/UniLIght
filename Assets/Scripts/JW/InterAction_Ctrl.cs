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
    [TextArea]
    public string[] antidepressant;
    [TextArea]
    public string[] Radio; // 표시할 메시지 배열

    [SerializeField] private Image DesImage;
    private string[] printStrings = null;
    [SerializeField] GameObject Sphere;
    [SerializeField] GameObject TextPanel;

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

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기
        #region Input매니저
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
            Debug.Log("코루틴 시작");
        }// 코루틴 시작
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
            if (hitObject.name == "궤도 폭격 조종기")
            {
                toggleText = true;
                printStrings = Obj_Cube;
            }
            if (hitObject.name == "사진")
            {
                toggleText = true;
                printStrings = Picture;
                DesImage.gameObject.SetActive(true);
                if (Resources.Load<Sprite>("Images/사진") != null)
                {
                    DesImage.sprite = Resources.Load<Sprite>("Images/사진");
                }

            }
            if (hitObject.name == "항우울제")
            {
                toggleText = true;
                printStrings = null;
            }
            if (hitObject.name == "라디오")
            {
                toggleText = true;
                printStrings = Radio;
            }

            if (hitObject.name == "소파")
            {
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = hitObject.transform.position;       // 목표 Transform의 위치를 종료 위치로 설정
                    endRotation = GetInfo().transform.rotation;       // 목표 Transform의 회전을 종료 회전으로 설정
                }
                journeyLength = Vector3.Distance(startPosition, endPosition); // 시작점과 종점 사이의 거리 계산
                startTime = Time.time;
                GetComponentInChildren<CameraSettings>().enabled = false;
                lerpPOV = true;
            }
            if (hitObject.name == "씬전환테스트")
            {
                startPosition = transform.position;
                startRotation = transform.rotation;
                if (GetInfo() != null)
                {
                    endPosition = hitObject.transform.position;       // 목표 Transform의 위치를 종료 위치로 설정
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
            raycastDistance = 0;
            if ((transform.position - endPosition).magnitude > 0.1f)
            {
                //씬전환 테스트
                if ((transform.position - endPosition).magnitude <= 0.3f && hitObject.name == "씬전환테스트" && SceneManager.GetActiveScene().name == "Jaewon_Test1")
                {
                    SceneManager.LoadScene("Jaewon_Test2");
                    break;
                }
                else if ((transform.position - endPosition).magnitude <= 0.3f && hitObject.name == "씬전환테스트" && SceneManager.GetActiveScene().name == "Jaewon_Test2")
                {
                    SceneManager.LoadScene("Jaewon_Test1");
                }

                // 이동 중
                float distCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
            }

            if ((transform.position - endPosition).magnitude <= 0.1f && pressE)
            {
                GetComponentInChildren<CameraSettings>().enabled = true;
                this.transform.position = startPosition;
                Debug.Log("원상복구" + startPosition + "/" + this.transform.position);
                lerpPOV = false;
                Debug.Log("이동 끝");
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
        #region 우울증약
        if (hitObject.name == "항우울제")
        {
            Debug.Log("항우울제 시작");
            Time.timeScale = 0f; // 게임 일시 정지
            pauseText.gameObject.SetActive(true); // 텍스트 활성화
            DesImage.gameObject.SetActive(true);
            if (Resources.Load<Sprite>("Images/antidepressant") != null)
            {
                DesImage.sprite = Resources.Load<Sprite>("Images/antidepressant");
                pauseText.text = "나른해지고 아무 생각이 들지 않게 된다.\n복용 하시겠습니까? \n (E를 눌러 복용/Q로 취소)";
            }
            else
            {
                Debug.Log("우울제 이미지 로드실패");
            }

            // 키 입력을 기다리는 루프
            bool done = false;

            while (!done)
            {
                yield return null; // 다음 프레임까지 기다림
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("복용");
                    if (Resources.Load<Sprite>("Images/swallow") != null)
                    {
                        DesImage.sprite = Resources.Load<Sprite>("Images/swallow");
                    }
                    else
                    {
                        Debug.Log("이미지 로드실패");
                    }
                    pauseText.text = "조금은 진정이 된다";
                    bool done2 = false;
                    while (!done2)
                    {
                        yield return new WaitForSecondsRealtime(1.5f);
                        toggleText = false;
                        done2 = true;
                    }
                    done = true; // 루프 종료
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    Debug.Log("복용취소");
                    toggleText = false;
                    done = true; // 루프 종료
                }
            }

            // 게임 재개
            Time.timeScale = 1.0f;
            TextPanel.SetActive(false);
            pauseText.gameObject.SetActive(false);
            DesImage.gameObject.SetActive(false);
            raycastDistance = 10;
        }
        #endregion
        #region 리스트 출력방식
        while (printStrings != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E키 입력");
                if (currentIndex < printStrings.Length)
                {
                    Time.timeScale = 0; // 게임 일시 정지
                    pauseText.gameObject.SetActive(true); // 텍스트 활성화
                    pauseText.text = printStrings[currentIndex]; // 현재 인덱스의 메시지 표시
                    currentIndex++; // 다음 메시지로 인덱스 증가
                }
                else
                {
                    Time.timeScale = 1.0f; // 게임 재개
                    pauseText.gameObject.SetActive(false); // 텍스트 숨기기
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