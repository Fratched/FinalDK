using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendNode : BehaviorNode
{
    public override bool Execute(EC_Damage enemy)
    {
        if (enemy != null)
        {
            // Apply defense, reducing incoming damage by 50%
            enemy.Defend(0.5f);  
            Debug.Log("Enemy is defending, reducing damage by 50%.");
            return true;  // Successfully defended
        }
        return false; // If there's no valid enemy to defend
    }
}