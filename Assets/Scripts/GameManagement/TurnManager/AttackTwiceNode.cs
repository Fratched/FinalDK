using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class AttackTwiceNode : BehaviorNode
    {
        public override bool Execute(EC_Damage enemy)
        {
            if (enemy != null)
            {
                enemy.Attack();  // First attack
                Debug.Log("Enemy attacked once!");

                enemy.Attack();  // Second attack
                Debug.Log("Enemy attacked twice!");
                return true;
            }

            Debug.Log("No enemy to attack.");
            return false;
        }
    }

