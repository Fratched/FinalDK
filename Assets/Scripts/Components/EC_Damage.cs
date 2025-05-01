using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Damage : MonoBehaviour
{
    private EC_Health health;
    public int damage;
    public int maxHealth;  // Max health for the enemy
    public int currentHealth;    // Current health for the enemy

    // Components
    [SerializeField] Counter counter;

    // For tracking if the enemy is defending
    private bool isDefending = false;
    private float defenseFactor = 0.5f;  // Default to 50% damage reduction
    public bool IsDefending => isDefending;
    public float DefenseFactor => defenseFactor;

    // Flag to track if the enemy was damaged in the last turn
    public bool WasDamaged { get; set; } = false;

    // Healing properties
    private float healFactor = 0.2f;  // Default to 20% of max health for healing

    private bool hasHealedThisTurn = false; // Flag to track if healing has been done this turn


    void Awake()
    {
        health = GetComponent<EC_Health>();
        if (health == null)
        {
            Debug.LogWarning("EC_Health component missing from enemy!");
        }
    }

    void Start()
    {
        currentHealth = maxHealth;  // Initialize health
        UpdateCounter();
    }

    public void Attack()
    {
        if (Player.instance?.Health == null)
        {
            Debug.LogWarning("Player instance or Health is missing. Cannot apply damage.");
            return;
        }

        int finalDamage = isDefending ? Mathf.FloorToInt(damage * defenseFactor) : damage;

        var anim = GetComponentInChildren<EC_Animator>();
        if (anim != null)
        {
            anim.Squash(1.5f, 1.5f);
        }
        else
        {
            Debug.LogWarning("EC_Animator not found in children of enemy.");
        }

        Player.instance.Health.Damage(finalDamage);
        Debug.Log(isDefending ? $"Enemy is defending, damage reduced to: {finalDamage}" : $"Enemy attacked with damage: {finalDamage}");

        WasDamaged = true;

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

            //Debug.Log("Enemy healed for: " + healAmount + ", current health: " + currentHealth);
        }
    }

    public void ResetHealingFlag()
    {
        hasHealedThisTurn = false;
    }

    void UpdateCounter()
    {
        if (counter == null)
        {
            counter.SetText(damage.ToString(), 1);
        }
        else
        {
            Debug.LogWarning("Counter reference is missing on EC_Damage.");
        }

    }

}