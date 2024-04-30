using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterAction_Ctrl : MonoBehaviour
{
    public float raycastDistance = 6f; //�ν��� �� �ִ� ����
    public Text pauseText; // Inspector���� �Ҵ�
    [TextArea]
    public string[] messages; // ǥ���� �޽��� �迭
    [SerializeField] GameObject Sphere;
    private int currentIndex = 0; // ���� �޽��� �ε���
    private bool toggleText = false;
    RaycastHit hit;
    Ray ray;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        ray = new Ray(transform.position, transform.forward); //�����ִ� �������� ���캸��

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺�� ���캸��

        if (Input.GetKeyDown(KeyCode.E)) //Ű���� E�� ������ ��
        {
            GameObject hitObject;
            if (Physics.Raycast(ray, out hit, raycastDistance)) //�ν��� �� �ִ� ���� �ȿ��� ��ü Ȯ��
            {
                hitObject = hit.collider.gameObject; //�ֺ� ��ü�� ������ �����ɴϴ�.

                if (hitObject != null) //��ü�� ���� ���
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
            Time.timeScale = 0f; // ���� �Ͻ� ����
            pauseText.text = messages[currentIndex]; // ���� �ε����� �޽��� ǥ��
            pauseText.gameObject.SetActive(true); // �ؽ�Ʈ Ȱ��ȭ
            currentIndex++; // ���� �޽����� �ε��� ����
        }
        else if(Time.timeScale ==0 && currentIndex == messages.Length)
        {
            Time.timeScale = 1.0f; // ���� �簳
            pauseText.gameObject.SetActive(false); // �ؽ�Ʈ �����
            toggleText = false;
        }
    }
}