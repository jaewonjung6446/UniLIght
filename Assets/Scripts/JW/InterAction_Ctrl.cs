using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 3f; //�ν��� �� �ִ� ����

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺�� ���캸��

        if (Input.GetKeyDown(KeyCode.E)) //Ű���� E�� ������ ��
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //�ν��� �� �ִ� ���� �ȿ��� ��ü Ȯ��
            {
                GameObject hitObject = hit.collider.gameObject; //�ֺ� ��ü�� ������ �����ɴϴ�.

                if (hitObject != null) //��ü�� ���� ���
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