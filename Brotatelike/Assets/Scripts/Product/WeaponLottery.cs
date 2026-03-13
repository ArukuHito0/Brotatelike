using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "WeaponLottery", menuName = "Lottery/WeaponLottery")]
public class WeaponLottery : ProductLotteryBase<WeaponData>
{
    protected override WeaponData RandomizeData()
    {
        return base.RandomizeData();
    }
}