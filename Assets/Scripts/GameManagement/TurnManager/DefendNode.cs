using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendNode : BehaviorNode
{
    public override bool Execute(EC_Damage enemy)
    {
        if (enemy == null || enemy.gameObject == null)
            return false;

        if (!enemy.gameObject.activeInHierarchy)
            return false;

        // Apply defense, reducing incoming damage
        enemy.Defend(0.5f);
        Debug.Log("Enemy is defending, reducing damage by 50%.");
        return true;
    }
}