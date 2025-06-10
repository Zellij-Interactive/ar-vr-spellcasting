using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Oculus.Voice;
using System.Reflection;
using Meta.WitAi.CallbackHandlers;

public class VoiceManager : MonoBehaviour
{
    [Header("Wit Configuration")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;
    [SerializeField] private WitResponseMatcher responseMatcher;
    [SerializeField] private TextMeshProUGUI transcriptionText;

    [Header("Voice Events")]
    [SerializeField] private UnityEvent wakeWordDetected;
    [SerializeField] private UnityEvent<string> completeTranscription;

    private SpellController[] spellControllers;
    private bool _voiceCommandReady;

    private void Awake()
    {
        // Cache spell controllers in the scene
        spellControllers = FindObjectsOfType<SpellController>();

        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(ReactivateVoice);
        appVoiceExperience.VoiceEvents.OnPartialTranscription.AddListener(OnPartialTranscription);
        appVoiceExperience.VoiceEvents.OnFullTranscription.AddListener(OnFullTranscription);

        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(responseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.AddListener(WakeWordDetected);
        }

        appVoiceExperience.Activate();
    }

    private void OnDestroy()
    {
        appVoiceExperience.VoiceEvents.OnRequestCompleted.RemoveListener(ReactivateVoice);
        appVoiceExperience.VoiceEvents.OnPartialTranscription.RemoveListener(OnPartialTranscription);
        appVoiceExperience.VoiceEvents.OnFullTranscription.RemoveListener(OnFullTranscription);

        var eventField = typeof(WitResponseMatcher).GetField("onMultiValueEvent", BindingFlags.NonPublic | BindingFlags.Instance);
        if (eventField != null && eventField.GetValue(responseMatcher) is MultiValueEvent onMultiValueEvent)
        {
            onMultiValueEvent.RemoveListener(WakeWordDetected);
        }
    }

    private void ReactivateVoice() => appVoiceExperience.Activate();

    private void WakeWordDetected(string[] args)
    {
        _voiceCommandReady = true;
        wakeWordDetected.Invoke();
    }

    private void OnPartialTranscription(string transcription)
    {
        if (!_voiceCommandReady) return;
        transcriptionText.text = transcription;

        Debug.Log($"Captured partial transcription: {transcription}");
    }

    private void OnFullTranscription(string transcription)
    {
        if (!_voiceCommandReady) return;
        completeTranscription.Invoke(transcription);

        Debug.Log($"Captured full transcription: {transcription}");


        if (_voiceCommandReady)
        {
            CastSpell(transcription);
            _voiceCommandReady = false; // Reset after full transcription
        }

        // _voiceCommandReady = false; // Reset after full transcription
    }

    private void CastSpell(string spellName)
    {
        Debug.Log($"Trying to cast spell: {spellName}");

        foreach (var controller in spellControllers)
        {
            controller.CastSpell(spellName);
        }
    }
}