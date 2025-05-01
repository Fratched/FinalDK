using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Damage : MonoBehaviour
{
    private EC_Health health;
    public int damage;
    public int maxHealth = 100;  // Max health for the enemy
    public int currentHealth;    // Current health for the enemy

    // Components
    [SerializeField] Counter counter;

    // For tracking if the enemy is defending
    private bool isDefending = false;
    private float defenseFactor = 0.5f;  // Default to 50% damage reduction

    // Flag to track if the enemy was damaged in the last turn
    public bool WasDamaged { get; set; } = false;

    // Healing properties
    private float healFactor = 0.2f;  // Default to 20% of max health for healing

    private bool hasHealedThisTurn = false; // Flag to track if healing has been done this turn


    void Awake()
    {
        health = GetComponent<EC_Health>();
    }

    void Start()
    {
        currentHealth = maxHealth;  // Initialize health
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
        
        // Set WasDamaged flag after attacking
        WasDamaged = true;  // Ensure this flag is set so the enemy can heal later
    }

    // Method to apply defense, reducing damage
    public void Defend(float defenseMultiplier)
    {
        isDefending = true;
        defenseFactor = defenseMultiplier;  // Set the defense multiplier 
        Debug.Log("Enemy is now defending, damage reduced by: " + defenseMultiplier * 100 + "%");
    }

    // Method to remove defense
    public void EndDefend()
    {
        isDefending = false;
        defenseFactor = 1f;  // Reset the defense factor to normal (no defense)
        Debug.Log("Enemy is no longer defending.");
    }

    // Method to heal the enemy (only if they were damaged)
    public void Heal(int healAmount)
    {

        if (hasHealedThisTurn)
        {
            Debug.Log("Enemy has already healed this turn.");
            return;
        }
        if (WasDamaged)
        {
           
            health.Heal(healAmount);  // Applies to actual EC_Health
            WasDamaged = false;  // Reset the damage flag after healing
            hasHealedThisTurn = true; // Set the flag to prevent healing again this turn

            Debug.Log("Enemy healed for: " + healAmount + ", current health: " + currentHealth);
        }
    }

    public void ResetHealingFlag()
    {
        hasHealedThisTurn = false;
    }

    void UpdateCounter()
    {
        if (counter == null) return;
        counter.SetText(damage.ToString(), 1);
    }
}