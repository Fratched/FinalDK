using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EC_Damage : MonoBehaviour
{
    public DamageNode root;
    private DamageNode selectedNode;

    [SerializeField] Counter counter;

    void Start()
    {
        BuildDamageTree();
        PickRandomAttack();
        UpdateCounter();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeAllDamage();
            PickRandomAttack(); // Update to a new random attack
            UpdateCounter();
        }
    }

    void BuildDamageTree()
    {
        root = new DamageNode("Base Hit", 5, 10);

        var fire = new DamageNode("Fire Strike", 8, 12);
        var venom = new DamageNode("Venom Bite", 7, 11);
        var explosion = new DamageNode("Explosion", 10, 15);

        root.children.Add(fire);
        root.children.Add(venom);
        venom.children.Add(explosion);

        Debug.Log("Damage tree built.");
    }

    void RandomizeAllDamage()
    {
        foreach (var node in root.GetAllUnlockedNodes())
        {
            // Customize random ranges per attack
            if (node.name == "Explosion") node.RandomizeDamage(12, 18);
            else if (node.name == "Fire Strike") node.RandomizeDamage(8, 14);
            else if (node.name == "Venom Bite") node.RandomizeDamage(6, 10);
            else node.RandomizeDamage(5, 12); // Base Hit or others

            Debug.Log($"Randomized: {node.name} = {node.damage}");
        }

        // After randomizing all, pick a new random attack to use
        PickRandomAttack();
    }

   
    
        void PickRandomAttack()
        {
            var allNodes = root.GetAllUnlockedNodes();
            selectedNode = allNodes[Random.Range(0, allNodes.Count)];
            Debug.Log($"Selected Attack: {selectedNode.name} ({selectedNode.damage} dmg)");

            // Make sure the attack damage reflects the latest randomized value
            UpdateCounter();
        }

    

    public void Attack()
    {
        GetComponentInChildren<EC_Animator>()?.Squash(1.5f, 1.5f);
        Player.instance.Health.Damage(selectedNode.damage);  // Using the updated damage here
        Debug.Log($"Enemy used {selectedNode.name} for {selectedNode.damage} damage.");
    }


    void UpdateCounter()
    {
        void UpdateCounter()
        {
            if (counter == null) return;
            // This will update the UI with the name of the selected attack and the new damage
            counter.SetText($"{selectedNode.name}: {selectedNode.damage}", 1);
        }

    }
}