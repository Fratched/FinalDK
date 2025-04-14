using UnityEngine;

[CreateAssetMenu(menuName = "Artifacts/Vengeful Ring")]
public class A_VengefulRing : A_Base
{
    public int damageIncrease;
    public int healthDecrease;

    public override void OnPickup()
    {
        triggered = true;
    }

    public override void Trigger()
    {
        Player.instance.Damage.AddDamageModifier(damageIncrease);
        Player.instance.Health.IncreaseHealth(-healthDecrease);
    }
}
