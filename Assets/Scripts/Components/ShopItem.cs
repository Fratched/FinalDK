using benjohnson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : ArtifactVisualizer
{
    [SerializeField] Counter costCounter;

    public override void Visualize(A_Base artifact)
    {
        base.Visualize(artifact);
    }

    public void Buy()
    {
        if (Player.instance.Wallet.money < artifact.cost)
        {
            SoundManager.instance.PlaySound("Not enough money");
            return;
        }

        ArtifactManager.instance.AddArtifact(artifact);
        Player.instance.Wallet.Buy(artifact.cost);

        Destroy(gameObject);
    }

    public void UpdateCounter(int value)
    {
        costCounter.SetText(artifact.cost.ToString(), value < artifact.cost ? 0 : 3);
    }

    public int GetPrice()
    {
        return artifact.cost;
    }


}
