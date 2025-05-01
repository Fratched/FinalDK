using benjohnson;
using UnityEngine;
using UnityEngine.Events;

public class EC_Health : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;
    EC_Damage damageComponent;

    // Flag to track if the entity has healed this turn
    public bool hasHealedThisTurn { get; private set; }

    [Header("Events")]
    public UnityEvent deathEvent;
    public UnityEvent damageEvent;

    // Components
    EC_Animator anim;
    [SerializeField] Counter counter;

    void Awake()
    {
        anim = GetComponentInChildren<EC_Animator>();

        currentHealth = maxHealth;
        UpdateCounter();

        damageComponent = GetComponent<EC_Damage>();
    }

    public void Damage(int value)
    {
        if (damageComponent != null && damageComponent.IsDefending)
        {
            value = Mathf.FloorToInt(value * damageComponent.DefenseFactor);
            Debug.Log("Enemy is defending! Incoming damage reduced to: " + value);
        }

        PlayerStats.instance.damageDealt += value;
        damageEvent.Invoke();
        DamagePopup.CreatePopup(transform.position, value);
        SoundManager.instance.PlaySound("Enemy Hurt");
        ArtifactManager.instance.TriggerDealDamage();

        currentHealth -= value;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
        UpdateCounter();
        anim?.Squash(1.5f, 0.75f);
    }

    public void Heal(int value)
    {
        if (hasHealedThisTurn)
        {
            Debug.Log("Enemy has already healed this turn.");
            return;
        }

        currentHealth += value;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        hasHealedThisTurn = true;  // Set the flag to prevent healing again this turn

        UpdateCounter();
    }

    void UpdateCounter()
    {
        if (counter == null) return;
        counter.SetText(currentHealth.ToString(), 0);
    }

    public void Kill()
    {
        PlayerStats.instance.enemiesDefeated++;
        deathEvent.Invoke();
        ArtifactManager.instance.TriggerKillEnemy();

        GetComponent<EC_Entity>().Remove();
    }
}
