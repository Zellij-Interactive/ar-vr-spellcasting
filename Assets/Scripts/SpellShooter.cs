using UnityEngine;
using UnityEngine.InputSystem;

public class SpellShooter : MonoBehaviour
{
    public GameObject spellPrefab;         // Assign the fireball prefab
    public Transform shootOrigin;          // Usually controller or camera
    public float shootForce = 500f;

    public PlayerMana manaSystem;
    public UIMessagePopup uiMessagePopup;  // Assign in Inspector

    void Update()
    {
        // Keyboard
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryCastSpell();
        }

        // Oculus/Meta: A button (Right Controller)
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            TryCastSpell();
        }
    }

    private void TryCastSpell()
    {
        if (manaSystem == null)
        {
            Debug.LogWarning("⚠️ Mana system not assigned!");
            return;
        }

        if (!manaSystem.CanCastFireball())
        {
            Debug.Log("❌ Not enough mana to cast fireball!");
            if (uiMessagePopup != null)
                uiMessagePopup.ShowMessage("Not enough mana!", 0.5f);
            return;
        }

        manaSystem.SpendFireballMana();
        ShootSpell();
    }

    public void ShootSpell()
    {
        if (spellPrefab == null || shootOrigin == null) return;

        GameObject spell = Instantiate(spellPrefab, shootOrigin.position, shootOrigin.rotation);

        // 🔒 Prevent instant collision with the player/controller
        Collider spellCol = spell.GetComponent<Collider>();
        Collider shooterCol = shootOrigin.GetComponentInParent<Collider>(); // Assumes shooter has a collider

        if (spellCol != null && shooterCol != null)
        {
            Physics.IgnoreCollision(spellCol, shooterCol);
        }

        Rigidbody rb = spell.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootOrigin.forward * shootForce);
        }

        Debug.Log("🔥 Fireball launched!");
    }
}
