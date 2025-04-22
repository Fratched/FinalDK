using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEnemyDisabledNode : BehaviorNode
{
    public override bool Execute(EC_Damage enemy)
    {
        if (GameCheats.EnemiesDisabled)
        {
            Debug.Log("Enemies are disabled. No action.");
            return false;
        }

        return true;
    }
}
