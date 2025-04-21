using benjohnson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerHealth Health { get; private set; }
    public PlayerDamage Damage { get; private set; }
    public PlayerWallet Wallet { get; private set; }

    // Assign the artifact in the Inspector
    public A_Base testArtifact;

    protected override void Awake()
    {
        base.Awake();

        Health = GetComponent<PlayerHealth>();
        Damage = GetComponent<PlayerDamage>();
        Wallet = GetComponent<PlayerWallet>();

        instance = this;
    }

    void Update()
    {

    }
}
