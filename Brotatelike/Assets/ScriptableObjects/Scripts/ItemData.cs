using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject, IUpgrade
{
    [Serializable]
    public struct UpgradeStats
    {
        public UpgradeStatus status;
        public int value;
    }

    public UpgradeStats[] stats;
    public TierType tier;
    public Sprite itemIcon;
    public string itemName;
    public uint itemPrice;

    public void Upgrade()
    {
        foreach (var item in stats)
        {
            item.status.ApplyStatusUP(item.value);
        }
    }

    public string GetUpgradeName()
    {
        var s = "";

        for (int i = 0; i < stats.Length; i++)
        {
            s += stats[i].status.GetUpgradeStatusName();

            if(i != stats.Length - 1) s += "\n";
        }

        return s;
    }

    public string GetUpgradeValueText()
    {
        var s = "";

        for (int i = 0; i < stats.Length; i++)
        {
            s += ValueToString(stats[i].value);

            if (i != stats.Length - 1) s += "\n";
        }

        return s;
    }

    public string GetEffectValueColor(float value)
    {
        return value > 0 ? "<color=green>+" : "<color=red>";
    }

    public string ValueToString(float value)
    {
        return GetEffectValueColor(value) + value.ToString();
    }
}
