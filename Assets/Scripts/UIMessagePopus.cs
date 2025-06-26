using UnityEngine;
using TMPro;

public class UIMessagePopup : MonoBehaviour
{
    [Header("UI References")]
    public GameObject panel;               // Assign UIMessagePanel
    public TextMeshProUGUI messageText;    // Assign UIMessageText

    public void ShowMessage(string message, float duration = 0.5f)
    {
        StopAllCoroutines(); // Stop previous timers

        if (panel != null) panel.SetActive(true);
        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
        }

        StartCoroutine(HideAfterSeconds(duration));
    }

    private System.Collections.IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (panel != null) panel.SetActive(false);
    }
}
