using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : BehaviorNode
{
    public override bool Execute(EC_Damage enemy)
    {
        if (enemy != null)
        {
            enemy.Attack();
            Debug.Log("Enemy attacked!");
            return true;
        }

        Debug.Log("No enemy to attack.");
        return false;
    }
}
