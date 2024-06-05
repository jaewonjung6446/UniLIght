using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GPT4UIManager : MonoBehaviour
{
    public Dropdown itemDropdown;
    public Button sendButton;
    public GPT4Request gpt4Request;

    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
        PopulateDropdown();
    }

    void PopulateDropdown()
    {
        itemDropdown.ClearOptions();
        List<string> options = new List<string> { "∫ÿ¥Î", "º“ø∞¡¶", "∏È∫¿", "¡¯≈Î¡¶" };
        itemDropdown.AddOptions(options);
    }

    void OnSendButtonClicked()
    {
        string selectedItem = itemDropdown.options[itemDropdown.value].text;
        gpt4Request.SendUrgentRequest(selectedItem);
    }
}