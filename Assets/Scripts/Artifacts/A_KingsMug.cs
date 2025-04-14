using UnityEngine;

[CreateAssetMenu(menuName = "Artifacts/Kings Mug")]
public class A_KingsMug : A_Base
{
    public int damageIncrease;

    public override void OnEnterBossRoom()
    {
        triggered = true;
    }

    public override void Trigger()
    {
        Player.instance.Damage.AddDamageModifier(damageIncrease);
    }
}
