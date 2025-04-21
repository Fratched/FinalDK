using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
            BehaviorNode tree = new SequenceNode(new List<BehaviorNode>
            {
                new IsEnemyDisabledNode(), // Check if enemy should act
                new AttackNode()           // Perform the attack
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
