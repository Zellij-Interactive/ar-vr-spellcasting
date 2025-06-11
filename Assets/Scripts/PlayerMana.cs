using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int maxMana = 250;
    public int currentMana;

    public int fireballCost = 50;
    public int regenAmount = 10;
    public float regenInterval = 2f;

    private float regenTimer;

    void Start()
    {
        currentMana = maxMana;
    }

    void Update()
    {
        regenTimer += Time.deltaTime;
        if (regenTimer >= regenInterval)
        {
            regenTimer = 0f;
            RegenerateMana();
        }
    }

    void RegenerateMana()
    {
        currentMana = Mathf.Min(currentMana + regenAmount, maxMana);
        // Debug.Log("🔄 Mana regenerated: " + currentMana);
    }

    public bool CanCastFireball()
    {
        return currentMana >= fireballCost;
    }

    public void SpendFireballMana()
    {
        currentMana -= fireballCost;
        Debug.Log("🔥 Fireball cast! Mana left: " + currentMana);
    }
}
