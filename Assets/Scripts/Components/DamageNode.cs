using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageNode
{
    public string name;
    public int damage;
    public bool isUnlocked;
    public List<DamageNode> children = new List<DamageNode>();

    // Constructor
    public DamageNode(string name, int min, int max)
    {
        this.name = name;
        this.damage = Random.Range(min, max); // Make sure max is exclusive
        this.isUnlocked = true;
        Debug.Log($"Created DamageNode: {name} with damage = {damage} (Range: {min}-{max})");
    }

    // Method to randomize the damage for this node
    public void RandomizeDamage(int min, int max)
    {
        // Debugging the range
        Debug.Log($"Randomizing {name}: Range ({min}-{max})");
        damage = Random.Range(min, max); // Unity's Random.Range
        Debug.Log($"Randomized {name}: New damage = {damage}");
    }

    // Method to get all unlocked nodes in the tree
    public List<DamageNode> GetAllUnlockedNodes()
    {
        List<DamageNode> result = new List<DamageNode>();
        if (isUnlocked)
            result.Add(this); // Add this node if unlocked
        foreach (var child in children)
            result.AddRange(child.GetAllUnlockedNodes()); // Add children recursively
        return result;
    }

    // Placeholder for unlocking nodes (not implemented yet)
    internal void UnlockNode()
    {
        // Unlocking logic will go here in the future
        throw new System.NotImplementedException();
    }
}
