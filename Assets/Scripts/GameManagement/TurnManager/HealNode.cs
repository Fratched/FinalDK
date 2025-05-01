using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNode : BehaviorNode
{
    public override bool Execute(EC_Damage enemy)
    {
        // Ensure the enemy has an EC_Entity component (or use other means to access the correct entity)
        EC_Entity entity = enemy.GetComponent<EC_Entity>();

        // If the enemy was damaged, allow healing
        if (entity != null)
        {
            // Heal the enemy (example: heal 10 points)
            int healAmount = 10;  // Fixed healing amount
            enemy.Heal(healAmount);
            Debug.Log("Enemy healed!");

            // Reset the damage flag after healing
            enemy.WasDamaged = false;

            return true;  // Indicate success
        }

        // If the enemy was not damaged, skip healing
        Debug.Log("No damage to heal.");
        return false;
    }
} 

