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
            Debug.Log("상호작용 불가능 상태");
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
        Time.timeScale = 0; // 게임 일시정지
        DesImage.enabled = true; // 텍스트 출력 시작
        DesImageIndex = -1; // 인덱스 초기화
        ShowNextImage(); // 첫 텍스트 출력
    }
    void ShowNextImage()
    {
        DesImageIndex++;

        if (DesImageIndex < DesImages.Count)
        {
            InterAction_Ctrl.Instance.DesTextAvailable = false;
            displayText.text = "간호장교를 위한 지침서가 적혀있는 매뉴얼이다.";
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
        Time.timeScale = 1; // 게임 재개
        InterAction_Ctrl.Instance.DesTextAvailable = true;
        DesImage.enabled = false; // 텍스트 출력 종료
        DesImageIndex = -1; // 인덱스 초기화
        displayText.gameObject.SetActive(false);
    }
}