using UnityEngine;

public class SpellController : MonoBehaviour
{
    [Header("Spell Shooter")]
    public SpellShooter spellShooter;

    [Header("Shield Equip")]
    public ShieldEquip shieldEquip;  // Assign this in the Inspector

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
                    Debug.LogWarning("⚠️ SpellShooter reference is not assigned!");
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
                    Debug.LogWarning("⚠️ ShieldEquip reference is not assigned!");
                }
                break;
            case "teleport":
                // Teleport logic
                break;
            default:
                Debug.Log("Unknown spell.");
                break;
        }
    }
}