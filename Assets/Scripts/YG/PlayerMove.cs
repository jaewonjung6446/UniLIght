using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpPower = 5;
    float yVelocity;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. y 속도에 중력을 계속 더하고싶다.
        yVelocity += gravity * Time.deltaTime;
        // 2. 만약 사용자가 점프버튼을 누르면 y속도에 뛰는 힘을 대입
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            yVelocity = jumpPower;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        // 카메라 방향보기
        dir = Camera.main.transform.TransformDirection(dir);

        // dir의 y 값을 0으로 설정하여 수평 이동만 처리
        dir.y = 0;

        // 이동 방향 벡터를 정규화
        dir.Normalize();

        // 3. 수평 이동과 y 속도를 별도로 처리
        Vector3 move = dir * speed * Time.deltaTime;
        move.y = yVelocity * Time.deltaTime;

        // P = P0 + vt
        cc.Move(move);
    }
}