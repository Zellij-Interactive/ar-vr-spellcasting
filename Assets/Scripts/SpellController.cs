using Oculus.Interaction.Locomotion;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpellController : MonoBehaviour
{
    [Header("Spell Shooter")]
    public SpellShooter spellShooter;

    [Header("Shield Equip")]
    public ShieldEquip shieldEquip;  // Assign this in the Inspector

    [Header("Teleport Settings")]
    public LayerMask teleportLayer; // Set to terrain layer
    public float maxTeleportDistance = 20f;

    public UIMessagePopup uiMessagePopup;  // Assign in Inspector

    public void CastSpell(string spellName)
    {
        Debug.Log($"Trying to cast spell: {spellName}");

        switch (spellName.ToLower())
        {
            case "fireball":
                if (spellShooter != null)
                {
                    Debug.Log("Casting fireball");

                    spellShooter.TryCastSpell();
                }
                else
                {
                    Debug.LogWarning("SpellShooter reference is not assigned!");
                }
                break;
            case "shield":
                if (shieldEquip != null)
                {
                    Debug.Log("Toggling shield");

                    shieldEquip.ToggleShield();
                }
                else
                {
                    Debug.LogWarning("ShieldEquip reference is not assigned!");
                }
                break;
            case "teleport":
                Debug.Log("Trying to teleport...");
                TryTeleportByRaycast();
                break;
            default:
                Debug.Log("Unknown spell.");
                if (uiMessagePopup != null)
                    uiMessagePopup.ShowMessage("Unknown spell!", 1f);
                break;
        }
    }

    private void TryTeleportByRaycast()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("No Main Camera found for teleport ray.");
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxTeleportDistance, teleportLayer))
        {
            TeleportPlayerTo(hit.point);
        }
        else
        {
            Debug.Log("No teleportable surface hit.");
            if (uiMessagePopup != null)
                uiMessagePopup.ShowMessage("No teleportable surface hit!", 1f);
        }
    }

    private void TeleportPlayerTo(Vector3 targetPoint)
    {
        OVRCameraRig rig = FindObjectOfType<OVRCameraRig>();
        if (rig == null)
        {
            Debug.LogWarning("No OVRCameraRig found in scene.");
            return;
        }

        // Maintain camera (head) height offset
        float heightOffset = rig.centerEyeAnchor.position.y - rig.transform.position.y;

        Vector3 newPosition = new Vector3(
            targetPoint.x,
            targetPoint.y,
            targetPoint.z
        );

        rig.transform.position = newPosition;
        Debug.Log($"Teleported to: {newPosition}");
    }

    private void Update()
    {
        DrawTeleportRay();
    }

    private void DrawTeleportRay()
    {
        if (Camera.main == null) {
            Debug.LogError("Camera main is null.");

            return; 
        }

        // Draw a ray from the camera forward direction
        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        Debug.DrawRay(origin, direction * maxTeleportDistance, Color.green, 0f, false);
    }
}