using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BehaviorNode
{
    private List<BehaviorNode> _nodes;

    public SelectorNode(List<BehaviorNode> nodes)
    {
        _nodes = nodes;
    }

    public override bool Execute(EC_Damage enemy)
    {
        foreach (var node in _nodes)
        {
            if (node.Execute(enemy))
            {
                return true; // Stop after the first successful execution
            }
        }

        return false; // All nodes failed
    }
}