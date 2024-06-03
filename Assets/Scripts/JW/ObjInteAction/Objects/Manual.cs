using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manual: MonoBehaviour, Obj_Interface
{
    private int DesImageIndex = -1;
    [SerializeField] private List<Sprite> DesImages;
    [SerializeField] private Image DesImage;
    [SerializeField] private Text displayText;
    [SerializeField] private MedicalInstruction medical;
    void Start()
    {
        DesImage.enabled = false;
    }
    public void InterAction()
    {
        if (medical.Instruction)
        {
            Debug.Log("��ȣ�ۿ� �Ұ��� ����");
        }
        if (Input.GetKeyDown(KeyCode.E) && !medical.Instruction)
        {
            if (!DesImage.enabled)
            {
                StartDisplayImage();
            }
            else
            {
                ShowNextImage();
            }
        }
        if (DesImage.enabled && Input.GetKeyDown(KeyCode.Q))
        {
            EndImage();
        }
    }
    void StartDisplayImage()
    {
        displayText.gameObject.SetActive(true);
        Time.timeScale = 0; // ���� �Ͻ�����
        DesImage.enabled = true; // �ؽ�Ʈ ��� ����
        DesImageIndex = -1; // �ε��� �ʱ�ȭ
        ShowNextImage(); // ù �ؽ�Ʈ ���
    }
    void ShowNextImage()
    {
        DesImageIndex++;

        if (DesImageIndex < DesImages.Count)
        {
            InterAction_Ctrl.Instance.DesTextAvailable = false;
            displayText.text = "��ȣ�屳�� ���� ��ħ���� �����ִ� �Ŵ����̴�.";
            DesImage.gameObject.SetActive(true);
            DesImage.sprite = DesImages[DesImageIndex];
        }
        else
        {
            EndImage();
        }
    }
    void EndImage()
    {
        Time.timeScale = 1; // ���� �簳
        InterAction_Ctrl.Instance.DesTextAvailable = true;
        DesImage.enabled = false; // �ؽ�Ʈ ��� ����
        DesImageIndex = -1; // �ε��� �ʱ�ȭ
        displayText.gameObject.SetActive(false);
    }
}