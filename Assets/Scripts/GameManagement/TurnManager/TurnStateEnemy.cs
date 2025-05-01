using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticPipe.Server.MonitorStats;


public class TurnStateEnemy : TurnBaseState
{
    public TurnStateEnemy(TurnManager context) : base(context) { }

    // Variables
    List<EC_Damage> _hostiles = new List<EC_Damage>();
    float _timeSinceAttack;

    public override void EnterState()
    {
        ArtifactManager.instance.TriggerStartOfEnemyTurn();
        _hostiles = DungeonManager.instance.CurrentRoom.GetHostiles();
        _timeSinceAttack = 0;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        // Check if cheat to disable enemies' turn is active
        if (GameCheats.EnemiesDisabled)
        {
            // Skip the enemy turn, go back to player turn
            SwitchState("Player");
            return;
        }

        if (_hostiles.Count > 0 && _timeSinceAttack >= _ctx.timeBetweenAttacks)
        {
            var enemy = _hostiles[0];

            // Create the behavior tree
            // Randomly decide which attack node to try first
            List<BehaviorNode> attackOptions = new List<BehaviorNode> { new AttackNode(), new AttackTwiceNode() };
            int randIndex = UnityEngine.Random.Range(0, attackOptions.Count);

            // Shuffle attack order randomly
            BehaviorNode first = attackOptions[randIndex];
            BehaviorNode second = attackOptions[1 - randIndex];

            // Create a selector that tries a random attack behavior
            BehaviorNode tree = new SequenceNode(new List<BehaviorNode>
            {
             new IsEnemyDisabledNode(),
             new SelectorNode(new List<BehaviorNode> { first, second }) // Tries one of the attack types
            });




            // Execute the tree on the first hostile (enemy)
            bool result = tree.Execute(enemy);
            if (!result)
            {
                Debug.Log("Enemy failed to act.");
            }

            // Remove the enemy from the list after attacking
            _hostiles.RemoveAt(0);
            _timeSinceAttack = 0; // Reset the timer
        }

        // Exit combat if room is cleared
        if (DungeonManager.instance.CurrentRoom.Clear)
        {
            ArtifactManager.instance.TriggerClearRoom();
            SwitchState("Idle");
            return;
        }

        // Switch to Player turn if no hostiles are left
        if (_hostiles.Count <= 0)
        {
            SwitchState("Player");
            return;
        }

        // Increment attack timer
        _timeSinceAttack += Time.deltaTime;
    }
}
