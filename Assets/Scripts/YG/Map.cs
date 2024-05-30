using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Ending_manager;

public class Map : MonoBehaviour, Obj_Interface
{
    private int DesImageIndex = -1;
    [SerializeField] private List<Sprite> DesImages;
    [SerializeField] private Image DesImage;
    [SerializeField] private Text displayText;
    [SerializeField] private GameObject Panel;

    private GameObject stack;
    Stack_Manager stackmanager;
    public void InterAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!DesImage.gameObject.activeSelf)
            {
                StartDisplayImage();
            }
            else
            {
                ShowNextImage();
            }
        }
        if (DesImage.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Q))
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
        Panel.gameObject.SetActive(true);
    }
    void ShowNextImage()
    {
        DesImageIndex++;

        if (DesImageIndex < DesImages.Count)
        {
            InterAction_Ctrl.Instance.DesTextAvailable = false;
            displayText.text = "지도 텍스트";
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
        stackmanager.checkmap();
        Time.timeScale = 1; // 게임 재개
        InterAction_Ctrl.Instance.DesTextAvailable = true;
        DesImage.gameObject.SetActive(false); // 텍스트 출력 종료
        DesImageIndex = -1; // 인덱스 초기화
        displayText.gameObject.SetActive(false);
        Panel.gameObject.SetActive(false);
    }
    void Start()
    {
        stack = GameObject.Find("StackManager");
        stackmanager = stack.GetComponent<Stack_Manager>();
    }
}