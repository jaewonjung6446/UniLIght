using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseManager : MonoBehaviour
{
    public string name;
    public Button myButton;
    public bool isClicked = false;
    [SerializeField] private GameObject textText;
    public GameObject image;
    private int currentIndex = 0;
    private Coroutine displayCoroutine;

    private void Awake()
    {
        image = GameObject.Find("InventoryImage");
        if (image != null)
        {
            SetImageAlpha(0); // �̹����� �����ϰ� ����
        }

        myButton = GetComponent<Button>();
        textText = GameObject.Find("InventoryText");

        if (myButton != null)
            Debug.Log("��ư ��ġ");
    }

    public void Update()
    {
        if (!isClicked)
            myButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        if (this.name == "����" && displayCoroutine == null)
        {
            Debug.Log(this.name);
            GlobalCoroutineRunner.StartGlobalCoroutine(Phone());
        }
        if (this.name == "antidepressant" && displayCoroutine == null)
        {
            Debug.Log(this.name);
            GlobalCoroutineRunner.StartGlobalCoroutine(DisplaySequence());
        }
        isClicked = false;
    }

    public IEnumerator Phone()
    {
        textText.GetComponent<Text>().text = "��ġ�� �ʹ� ���� ���Ͽ��� �����Ͱ� ����� �۵����� �ʾ� ���������̴�.";
        yield return new WaitForSecondsRealtime(1.5f);
        textText.GetComponent<Text>().text = "";
        displayCoroutine = null;
    }

    private IEnumerator DisplaySequence()
    {
        Time.timeScale = 0;
        currentIndex = 0;
        Debug.Log(Gamemanager.Instance.images.Length);

        while (currentIndex < 2)
        {
            // ���� �̹����� �ؽ�Ʈ ǥ��
            image.GetComponent<Image>().sprite = Gamemanager.Instance.images[currentIndex];
            SetImageAlpha(1.0f); // �̹����� ���̰� ����
            Debug.Log("��� ����");
            textText.GetComponent<Text>().text = Gamemanager.Instance.texts[currentIndex];

            // Ű �Է� ���
            bool next = false;
            while (!next)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    next = true;
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    ClearDisplay();
                    yield break;
                }
                yield return null;
            }

            // ���� �̹��� ����
            SetImageAlpha(0.0f); // �̹����� �ٽ� �����ϰ� ����
            currentIndex++;
        }

        // ��� �̹����� �ؽ�Ʈ�� ������ ��
        ClearDisplay();
    }

    private void SetImageAlpha(float alpha)
    {
        if (image != null)
        {
            Image img = image.GetComponent<Image>();
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }

    private void ClearDisplay()
    {
        if (currentIndex < 2)
        {
            SetImageAlpha(0.0f); // �̹����� �����ϰ� ����
        }
        textText.GetComponent<Text>().text = "";
        Time.timeScale = 1;
        displayCoroutine = null;
    }
}
