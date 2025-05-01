using UnityEngine;
using UnityEngine.Events;

public class EC_Entity : Clickable
{
    public EC_Health health;
    public EC_Damage damage;


    [HideInInspector] public Room room;

    public UnityEvent removeEvent;

    [Header("FX")]
    public GameObject deathFX;


    void Awake()
    {
        // Initialize or reference components
        health = GetComponent<EC_Health>();
        damage = GetComponent<EC_Damage>();
    }

    public void IsEnabled(bool enabled)
    {
        transform.parent = enabled ? DungeonManager.instance.transform : null;
        gameObject.SetActive(enabled);
    }

    public void Remove()
    {
        removeEvent.Invoke();

        if (deathFX != null)
        {
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(fx, 2);
        }

        room.roomEntities.Remove(this);
        IsEnabled(false);
        DungeonManager.instance.GetComponent<ArrangeGrid>().Arrange();

        Destroy(gameObject);
    }
}
