using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BehaviorNode
{
    private List<BehaviorNode> _nodes;

    public SequenceNode(List<BehaviorNode> nodes)
    {
        _nodes = nodes;
    }

    public override bool Execute(EC_Damage enemy)
    {
        foreach (var node in _nodes)
        {
            if (!node.Execute(enemy))
            {
                return false;
            }
        }
        return true;
    }
}
