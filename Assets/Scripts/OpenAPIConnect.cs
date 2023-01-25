using OpenAI_API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OpenAPIConnect : MonoBehaviour
{
    private OpenAIAPI api;
    public string Request;
    public string APIResponse;
    public TextMeshProUGUI m_TestOutput;
    private void Start()
    {
        api = new OpenAIAPI("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"); // shorthand
        GetRequest();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GetRequest();
        }
    }

    private async void GetRequest()
    {
        APIResponse = "";

        await api.Completions.StreamCompletionAsync
            (
                new CompletionRequest(Request, Model.DavinciText, 200, 0.5, presencePenalty: 0.1, frequencyPenalty: 0.1),
                res => UpdateText(res.ToString())
            );
    }

    private void UpdateText(string text)
    {
        APIResponse += text;
        m_TestOutput.text = APIResponse;
    }
}
