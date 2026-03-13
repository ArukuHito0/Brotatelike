using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "ItemLottery", menuName = "Lottery/ItemLottery")]
public class ItemLottery : ProductLotteryBase<ItemData>
{
    protected override ItemData RandomizeData()
    {
        return base.RandomizeData();
    }
}