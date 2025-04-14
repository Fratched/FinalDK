using UnityEngine;

[CreateAssetMenu(menuName = "Artifacts/Dying Heart")]
public class A_DyingHeart : A_Base
{
    public int loseAmount;

    public int hpGain;

    public int damageGain;

    public override void OnEnterRoom()
    {
        triggered = true;
    }
    
    public override void OnPickup()
    {
        triggered = true;

        Player.instance.Health.IncreaseHealth(hpGain);
        Player.instance.Damage.AddDamageModifier(damageGain);
    }

    public override void Trigger()
    {
        Player.instance.Health.IncreaseHealth(-loseAmount);
    }
}
