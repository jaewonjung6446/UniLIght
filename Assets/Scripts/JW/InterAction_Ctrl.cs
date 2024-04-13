using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 3f; //인식할 수 있는 범위

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        ray = new Ray(transform.position, transform.forward); //보고있는 방향으로 살펴보기

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스로 살펴보기

        if (Input.GetKeyDown(KeyCode.E)) //키보드 E를 눌렀을 때
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //인식할 수 있는 범위 안에서 물체 확인
            {
                GameObject hitObject = hit.collider.gameObject; //주변 물체의 정보를 가져옵니다.

                if (hitObject != null) //물체가 있을 경우
                {
                    if (hitObject.GetComponent<InventoryManager>() != null)
                    {
                        hitObject.GetComponent<InventoryManager>().AddToInventory(hitObject);
                    }
                }
            }
        }
    }
}