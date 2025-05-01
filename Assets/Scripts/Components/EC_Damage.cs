using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Damage : MonoBehaviour
{
    public int damage;

    // Components
    [SerializeField] Counter counter;

    // For tracking if the enemy is defending
    private bool isDefending = false;
    private float defenseFactor = 0.5f;  // Default to 50% damage reduction

    void Start()
    {
        UpdateCounter();
    }

    public void Attack()
    {
        if (isDefending)
        {
            // If defending, reduce the damage by the defense factor
            int reducedDamage = Mathf.FloorToInt(damage * defenseFactor);
            GetComponentInChildren<EC_Animator>()?.Squash(1.5f, 1.5f);
            Player.instance.Health.Damage(reducedDamage);  // Apply reduced damage
            Debug.Log("Enemy is defending, damage reduced to: " + reducedDamage);
        }
        else
        {
            // Normal attack logic if not defending
            GetComponentInChildren<EC_Animator>()?.Squash(1.5f, 1.5f);
            Player.instance.Health.Damage(damage);
            Debug.Log("Enemy attacked with damage: " + damage);
        }
    }

    // Method to apply defense, reducing damage
    public void Defend(float defenseMultiplier)
    {
        isDefending = true;
        defenseFactor = defenseMultiplier;  // Set the defense multiplier (e.g., 0.5f for 50%)
        Debug.Log("Enemy is now defending, damage reduced by: " + defenseMultiplier * 100 + "%");
    }

    // Method to remove defense
    public void EndDefend()
    {
        isDefending = false;
        defenseFactor = 1f;  // Reset the defense factor to normal (no defense)
        Debug.Log("Enemy is no longer defending.");
    }

    void UpdateCounter()
    {
        if (counter == null) return;
        counter.SetText(damage.ToString(), 1);
    }
}