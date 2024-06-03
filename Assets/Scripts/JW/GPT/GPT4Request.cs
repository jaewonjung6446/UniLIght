using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GPT4Request : MonoBehaviour
{
    public string apiKey; // 여기에 OpenAI API Key를 입력하세요.
    private const string apiUrl = "https://api.openai.com/v1/chat/completions"; // 올바른 URL로 수정
    [SerializeField] MedicalInstruction medical;
    public string res;

    void Start()
    {
        // 환경 변수에서 API 키를 가져옴
        if (string.IsNullOrEmpty(apiKey))
        {
            medical.instructions.text = ("API Key is not set in environment variables.");
        }
        else
        {
            medical.instructions.text = ("API Key successfully loaded.");
        }
    }

    [System.Serializable]
    public class OpenAIMessage
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class OpenAIRequest
    {
        public string model = "gpt-4";
        public OpenAIMessage[] messages;
    }

    [System.Serializable]
    public class Choice
    {
        public OpenAIMessage message;
    }

    [System.Serializable]
    public class OpenAIResponse
    {
        public Choice[] choices;
    }

    public IEnumerator GetGPT4Response(string prompt, System.Action<string> callback)
    {
        OpenAIMessage userMessage = new OpenAIMessage
        {
            role = "user",
            content = prompt
        };

        OpenAIRequest requestData = new OpenAIRequest
        {
            messages = new OpenAIMessage[] { userMessage }
        };

        string jsonBody = JsonUtility.ToJson(requestData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            Debug.Log("Sending request to OpenAI API...");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error in API request: " + request.error);
                callback?.Invoke(null);
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                Debug.Log("API Response: " + jsonResponse);

                try
                {
                    OpenAIResponse response = JsonUtility.FromJson<OpenAIResponse>(jsonResponse);
                    callback?.Invoke(response.choices[0].message.content.Trim());
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error parsing API response: " + ex.Message);
                    callback?.Invoke(null);
                }
            }
        }
    }

    public IEnumerator SendUrgentRequest(string item)
    {
        string prompt = $"In an urgent wartime scenario, urgently ask a nurse for {item}in korean.";
        bool responseReceived = false;
        string responseText = null;

        yield return StartCoroutine(GetGPT4Response(prompt, response =>
        {
            responseText = response;
            responseReceived = true;
        }));

        if (responseReceived && responseText != null)
        {
            Debug.Log("GPT-4 응답: " + responseText);
            medical.instructions.text = responseText;
        }
        else
        {
            Debug.LogError("Failed to get response from GPT-4.");
        }
    }
}
