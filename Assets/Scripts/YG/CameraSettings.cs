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
    public float initialHeight = 0.8f; // 초기 카메라 높이

    public GameObject player;
    private CharacterController cc; // 플레이어의 캐릭터컨트롤러 컴포넌트를 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.localPosition = new Vector3(0, initialHeight, 0); // 초기 카메라 높이 설정
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 회전
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;

        rx = Mathf.Clamp(rx, -80, 80);

        transform.eulerAngles = new Vector3(-rx, ry, 0);

        // 플레이어의 위치를 기준으로 카메라 위치 설정
        transform.position = player.transform.position + new Vector3(0, transform.localPosition.y, 0);

        // 플레이어가 움직일 때 카메라가 위아래로 흔들리도록 함
        if (cc.velocity.magnitude > walkSpeedThreshold && cc.isGrounded)
        {
            float yOffset = initialHeight + Mathf.Sin(Time.time * 15f) * walkHeightOffset; // 시간에 따라 위아래로 흔들리도록 함
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Lerp(transform.localPosition.y, yOffset, smoothSpeed * Time.deltaTime); // 부드러운 이동 적용
            transform.localPosition = newPosition;
        }
        else
        {
            // 걷는 동작이 멈추면 카메라의 높이를 초기 높이로 부드럽게 되돌림
            Vector3 newPosition = transform.localPosition;
            newPosition.y = Mathf.Lerp(transform.localPosition.y, initialHeight, smoothSpeed * Time.deltaTime); // 부드러운 이동 적용
            transform.localPosition = newPosition;
        }
    }
}