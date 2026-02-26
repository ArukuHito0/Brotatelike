using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeData")]
public class UpgradeData : ScriptableObject, IUpgrade
{
    [Serializable]
    public struct UpgradeStats
    {
        public UpgradeStatus status;
        public int value;
    }

    public UpgradeStats[] upgrades;
    public TierType tier;
    public Sprite upgradeIcon;
    public string upgradeName;

    public void Upgrade()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.status.ApplyStatusUP(upgrade.value);
        }
    }

    public string GetUpgradeName()
    {
        var s = "";

        for (int i = 0; i < upgrades.Length; i++)
        {
            s += upgrades[i].status.GetUpgradeStatusName();

            if(i != upgrades.Length - 1) s += "\n";
        }

        return s;
    }

    public string GetUpgradeValueText()
    {
        var s = "";

        for (int i = 0; i < upgrades.Length; i++)
        {
            s += ValueToString(upgrades[i].value);

            if (i != upgrades.Length - 1) s += "\n";
        }

        return s;
    }

    protected string GetEffectValueColor(float value)
    {
        return value > 0 ? "<color=green>+" : "<color=red>-";
    }

    protected string ValueToString(float value)
    {
        return GetEffectValueColor(value) + value.ToString();
    }
}
