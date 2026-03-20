using System;
using System.Collections;
using UnityEngine;

public class CoinItem : PickableItem
{
    [SerializeField] private uint gold;

    protected override void OnPickUpItem()
    {
        PlayerController.Instance.wallet.AddMoney(gold);
    }
}
