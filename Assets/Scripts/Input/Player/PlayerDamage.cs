using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private LinkedList<int> damageModifiers = new LinkedList<int>();  // Use LinkedList instead of List

    [SerializeField] Counter counter;
    [SerializeField] A_SharpenedSword sharpenedSwordArtifact;  // Reference to the Sharpened Sword artifact

    void Start()
    {
        // Adding initial damage value (if necessary)
        damageModifiers.AddLast(4);  // Initial damage value
        UpdateCounter();
    }

    // Add a new damage modifier to the linked list
    public void AddDamageModifier(int amount)
    {
        int currentDamage = GetTotalDamage();

        if (currentDamage >= int.MaxValue)
        {
            // Already at max value, can't add more
            return;
        }

        // Clamp the amount so we don’t go over int.MaxValue
        int clampedAmount = Mathf.Min(amount, int.MaxValue - currentDamage);
        damageModifiers.AddLast(clampedAmount);
        UpdateCounter();
    }


    // Remove a specific modifier from the linked list
    public void RemoveModifier(LinkedListNode<int> node)
    {
        damageModifiers.Remove(node);  // Remove the specific node (modifier)
        UpdateCounter();
    }

    // Infinite damage algorithm: Add a huge modifier or apply scaling damage as needed
    public void ApplyInfiniteDamage()
    {
        int currentDamage = GetTotalDamage();

        if (currentDamage >= int.MaxValue)
        {
            // Already at max, nothing to do
            return;
        }

        int infiniteDamage = int.MaxValue - currentDamage; // Calculate how much damage can be added without overflow.
        damageModifiers.AddLast(infiniteDamage);  // Add the remaining damage that won’t cause an overflow

        UpdateCounter();
    }


    // Add the Sharpened Sword artifact and apply its effect
    public void AddSharpenedSwordArtifact()
    {
        if (sharpenedSwordArtifact != null)
        {
            sharpenedSwordArtifact.Trigger();  // Trigger the effect of the Sharpened Sword artifact
        }
        UpdateCounter();
    }

    // Apply damage to a target using the current total damage
    public void Damage(EC_Health _target)
    {
        _target.Damage(GetTotalDamage());  // Deal total damage to the target
    }

    // Calculate the total damage by summing all modifiers in the linked list
    public int GetTotalDamage()
    {
        int total = 0;
        foreach (var dmg in damageModifiers)
            total += dmg;  // Sum all damage modifiers
        return total;
    }

    // Update the counter with the total damage value
    void UpdateCounter()
    {
        if (counter == null) return;
        counter.SetText(GetTotalDamage().ToString(), 1);  // Update UI counter with total damage
    }

    // Detect key press to apply infinite damage
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  // Check if the "1" key is pressed
        {
            ApplyInfiniteDamage();  // Apply infinite damage when key is pressed
        }
        // Check for key press to add the Sharpened Sword artifact (2 key)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddSharpenedSwordArtifact();  // Add the Sharpened Sword damage increase
        }
    }

}
