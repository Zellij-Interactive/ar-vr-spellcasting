using UnityEngine;

public class SpellController : MonoBehaviour
{
    [Header("Spell Shooter")]
    public SpellShooter spellShooter;  // Assign this in the Inspector

    public void CastSpell(string spellName)
    {
        Debug.Log($"Casting spell: {spellName}");

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
                // Shield logic
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