using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 6f; //인식할 수 있는 범위
    public Text pauseText; // Inspector에서 할당
    [TextArea]
    public string[] messages; // 표시할 메시지 배열
    [SerializeField] GameObject Sphere;
    private int currentIndex = 0; // 현재 메시지 인덱스
    private bool toggleText = false;
    RaycastHit hit;
    Ray ray;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스로 살펴보기

        if (Input.GetKeyDown(KeyCode.E)) //키보드 E를 눌렀을 때
        {
            GameObject hitObject;
            if (Physics.Raycast(ray, out hit, raycastDistance)) //인식할 수 있는 범위 안에서 물체 확인
            {
                hitObject = hit.collider.gameObject; //주변 물체의 정보를 가져옵니다.

                if (hitObject != null) //물체가 있을 경우
                {
                    if (hitObject.GetComponent<InventoryManager>() != null)
                    {
                        hitObject.GetComponent<InventoryManager>().AddToInventory(hitObject);
                        toggleText = true;
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && toggleText){
            Sphere.SetActive(true);
            ToggleText();
        }
    }
    void ToggleText()
    {
        if (currentIndex < messages.Length)
        {
            Time.timeScale = 0f; // 게임 일시 정지
            pauseText.text = messages[currentIndex]; // 현재 인덱스의 메시지 표시
            pauseText.gameObject.SetActive(true); // 텍스트 활성화
            currentIndex++; // 다음 메시지로 인덱스 증가
        }
        else if(Time.timeScale ==0 && currentIndex == messages.Length)
        {
            Time.timeScale = 1.0f; // 게임 재개
            pauseText.gameObject.SetActive(false); // 텍스트 숨기기
            toggleText = false;
        }
    }
}