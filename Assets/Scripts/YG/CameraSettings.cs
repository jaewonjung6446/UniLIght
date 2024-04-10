using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    float rx, ry;
    public float rotSpeed = 200f;
    public float walkSpeedThreshold = 0.1f; // 걷는 동작을 감지하기 위한 최소 속도
    public float walkHeightOffset = 0.5f; // 걷는 동작시 카메라의 높이 조절을 위한 변수
    public float smoothSpeed = 5f; // 카메라 높이 조절의 부드러운 이동을 위한 변수
    public float Inity = 0.8f; // 초기 카메라 높이
    //private bool trig = true; //화면 울렁거림 트리거
    //참고 : https://hannom.tistory.com/205

    public GameObject player;
    private CharacterController cc; //플레이어의 캐릭터컨트롤러 컴포넌트를 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.localPosition = new Vector3(transform.position.x,Inity ,transform.position.z); // 초기 카메라 높이 설정
    }

    // Update is called once per frame
    void Update()
    {
        //카메라 rotate
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;

        rx = Mathf.Clamp(rx, -80, 80);

        transform.eulerAngles = new Vector3(-rx, ry, 0);

        //플레이어가 움직일때 캐릭터가 위아래로 흔들리고싶다
        //움직일때 시간에 따라 진동하려면?
        // sin 그래프를 이용해 시간에 따라 위아래로 흔들리도록 함
        Vector3 newPosition = transform.localPosition;
        if (cc.velocity.magnitude > walkSpeedThreshold && cc.isGrounded)
        {
            float yOffset = Inity + Mathf.Sin(Time.time * 15f) * walkHeightOffset; // 시간에 따라 위아래로 흔들리도록 함
            newPosition.y = Mathf.Lerp(transform.localPosition.y, yOffset, smoothSpeed * Time.deltaTime); // 부드러운 이동 적용
            transform.localPosition = newPosition;
        }
        else
        {
            // 걷는 동작이 멈추면 카메라의 높이를 초기 높이로 부드럽게 되돌림
            newPosition.y = Mathf.Lerp(transform.localPosition.y, Inity, smoothSpeed * Time.deltaTime); // 부드러운 이동 적용
            transform.localPosition = newPosition;
        }
    }
}
