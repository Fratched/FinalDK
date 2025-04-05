using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int damage;

    // Components
    [SerializeField] Counter counter;

    void Start()
    {
        UpdateCounter();
    }

    public void IncreaseDamage(int increase)
    {
        damage += increase;
        UpdateCounter();
    }

    public void Damage(EC_Health _target)
    {
        _target.Damage(damage);
    }

    void UpdateCounter()
    {
        if (counter == null) return;
        counter.SetText(damage.ToString(), 1);
    }
}
