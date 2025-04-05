using benjohnson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    // Components
    ArrangeGrid gridLayout;

    [SerializeField] GameObject shopItemPrefab;

    List<ShopItem> shopItems = new List<ShopItem>();

    bool sortAscending = true;

    protected override void Awake()
    {
        base.Awake();
        gridLayout = GetComponent<ArrangeGrid>();
    }

    void Start()
    {
        if (GameManager.instance.stage >= 4)
            GameManager.instance.LoadWinScreen();

        LoadShop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SortToggle();
        }
    }

    void LoadShop()
    {
        shopItems.Clear();

        List<A_Base> artifacts = ArtifactManager.instance.GetRandomArtifacts(6);
        foreach (A_Base a in artifacts)
        {
            ShopItem item = Instantiate(shopItemPrefab, transform).GetComponent<ShopItem>();
            item.Visualize(a);
            shopItems.Add(item);
        }

        SortShop(); // Optional: sort immediately on load
    }

    public void SortToggle()
    {
        sortAscending = !sortAscending;
        SortShop();
    }

    public void SortShop()
    {
        shopItems.Sort((a, b) =>
        {
            int priceA = a.GetPrice();
            int priceB = b.GetPrice();
            return sortAscending ? priceA.CompareTo(priceB) : priceB.CompareTo(priceA);
        });

        for (int i = 0; i < shopItems.Count; i++)
        {
            shopItems[i].transform.SetSiblingIndex(i);
        }

        gridLayout.Arrange();
        ReloadPrices();
    }

    public void ReloadPrices()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ShopItem>().UpdateCounter(Player.instance.Wallet.money);
        }
    }

    public void ExitShop()
    {
        GameManager.instance.LoadNextStage();
    }
}
