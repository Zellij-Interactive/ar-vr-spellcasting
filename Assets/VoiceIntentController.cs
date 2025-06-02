using Oculus.Voice;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceIntentController : MonoBehaviour
{

    // Add AppVoiceExperience and VoiceIntentController components to the GameObject
    [Header("Voice")]
    [SerializeField]
    private AppVoiceExperience appVoiceExperience;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI fullTranscriptText;

    [SerializeField]
    private TextMeshProUGUI partialTranscriptText;

    private ShapeController[] controllers;

    private bool appVoiceActive;

    private void Awake()
    {
        appVoiceExperience = GetComponent<AppVoiceExperience>();
        controllers = FindObjectsOfType<ShapeController>();
        fullTranscriptText.text = partialTranscriptText.text = string.Empty;

        // bind transcriptions and activate state

        appVoiceExperience.events.onFullTranscription.AddListener((transcription) =>
        {
            fullTranscriptText.text = transcription;
        });

        appVoiceExperience.events.onPartialTranscription.AddListener((transcription) =>
        {
            partialTranscriptText.text = transcription;
        });

        appVoiceExperience.events.onRequestCreated.AddListener((request) =>
        {
            appVoiceActive = true;
            Logger.Instance.LogInfo("OnRequestCreated Active");
        });

        appVoiceExperience.events.onRequestCompleted.AddListener(() =>
        {
            appVoiceActive = false;
            Logger.Instance.LogInfo("OnRequestCompleted Active");
        });
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !appVoiceActive)
        {
            // activate voice experience
            appVoiceExperience.Activate();
        }
    }

    public void CastSpell(String[] info)
    {
        DisplayValues("CastSpell:", info);
        // cast spell info based on intent response

        if (info.Length > 0)
        {
            foreach (var controller in controllers)
            {
                controller.CastSpell(info[0]);
            }
        }
    }

    private static void DisplayValues(string prefix, string[] info)
    {
        foreach (var i in info)
        {
            Logger.Instance.LogInfo($"{prefix} {i}");
        }
    }
}
