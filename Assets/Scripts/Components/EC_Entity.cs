using UnityEngine;
using UnityEngine.Events;

public class EC_Entity : Clickable

{


    [HideInInspector] public Room room;

    public UnityEvent removeEvent;

    [Header("FX")]
    public GameObject deathFX;

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
