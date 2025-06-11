using UnityEngine;

public class ShieldEquip : MonoBehaviour
{
    public GameObject shieldPrefab;         // Drag your Shield prefab here
    public Transform leftHandAnchor;        // Drag LeftHandAnchor from your XR rig

    private GameObject currentShield;

    void Update()
    {
        // Press B button (Right Controller) to toggle the shield
        if (OVRInput.GetDown(OVRInput.Button.Two)) // Button.Two = B
        {
            ToggleShield();
        }
    }

    public void ToggleShield()
    {
        if (currentShield == null)
        {
            EquipShield();
        }
        else
        {
            Destroy(currentShield);
            currentShield = null;
        }
    }

    void EquipShield()
    {
        if (shieldPrefab != null && leftHandAnchor != null)
        {
            currentShield = Instantiate(shieldPrefab, leftHandAnchor);

            // Reset local transform
            currentShield.transform.localPosition = Vector3.zero;
            currentShield.transform.localRotation = Quaternion.identity;

            // Adjust position and orientation for proper fit
            currentShield.transform.localPosition = new Vector3(0f, 0f, 0.15f);
            currentShield.transform.localRotation = Quaternion.Euler(0f, -90f, 90f);
        }
    }
}
