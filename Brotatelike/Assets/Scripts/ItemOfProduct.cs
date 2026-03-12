using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ItemOfProduct : ProductBase
{
    public ItemData data { get; set; }

    public override TierType Tier => data.ItemTier;
    public override Sprite Icon => data.ItemIcon;
    public override string Name => data.ItemName;
    public override uint Price => data.ItemPrice;

    public override void PayProduct()
    {
        foreach (var item in data.Stats)
        {
            item.status.ApplyStatusUP(item.value);
        }
    }

    public override string GetDescriptionText()
    {
        var s = string.Empty;

        for (int i = 0; i < data.Stats.Length; i++)
        {
            s += data.Stats[i].status.GetPlayerStatusName();
            s += "  " + data.Stats[i].value.ToColorText();

            if (i != data.Stats.Length - 1) s += "\n";
        }

        return s;
    }
}