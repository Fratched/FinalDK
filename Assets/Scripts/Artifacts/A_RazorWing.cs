using benjohnson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Artifacts/Razor Wing")]
public class A_RazorWing : A_Base
{
    [Range(0f, 1f)] public float probability;

    public int damageIncrease;

    public override void OnDealDamage()
    {
        bool passed = Utilities.TestProbability(probability);
        if (passed)
            triggered = true;
    }

    public override void Trigger()
    {
        Player.instance.Damage.AddDamageModifier(damageIncrease);
    }
}
