using UnityEngine;
using TMPro;

public class UIMessagePopup : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public void ShowMessage(string message, float duration = 0.5f)
    {
        StopAllCoroutines(); // Stop previous timers
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        StartCoroutine(HideAfterSeconds(duration));
    }

    private System.Collections.IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        messageText.gameObject.SetActive(false);
    }
}
