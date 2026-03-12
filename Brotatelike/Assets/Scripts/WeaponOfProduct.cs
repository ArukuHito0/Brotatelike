using UnityEngine;

public class WeaponOfProduct : ProductBase
{
    public WeaponData data;

    public override TierType Tier => data.WeaponTier;
    public override Sprite Icon => data.WeaponIcon;
    public override string Name => data.WeaponName;
    public override uint Price => data.WeaponPrice;

    public override void PayProduct()
    {

    }

    public override string GetDescriptionText()
    {
        string text = string.Empty;
        text = $"ダメージ: \n" +
               $"クリティカル率: %\n" +
               $"クリティカルダメージ: x\n" +
               $"クールダウン: s\n" +
               $"射程距離: m\n";

        return text;
    }
}
