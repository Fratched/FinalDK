using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealNode : BehaviorNode
{
    public override bool Execute(EC_Damage enemy)
    {
        // If the enemy was damaged, allow healing
        if (enemy.WasDamaged)
        {
            // Heal the enemy (example: heal 10 points)
            enemy.Heal();
            Debug.Log("Enemy healed!");
            return true;  // Indicate success
        }

        // If the enemy was not damaged, skip healing
        Debug.Log("No damage to heal.");
        return false;
    }
} 

