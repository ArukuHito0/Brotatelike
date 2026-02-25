using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "UpgradeData/")]
public abstract class UpgradeBaseData : ScriptableObject, IUpgrade
{
    public TierType tier;
    public Sprite upgradeIcon;
    public string upgradeName;

    public virtual void Upgrade(PlayerController player) { }
    public virtual string GetEffectName() { return null; }
    public virtual string GetEffectValue() { return null; }

    protected string GetEffectValueColor(float value)
    {
        return value > 0 ? "<color=green>+" : "<color=red>-";
    }

    protected string ValueToString(float value)
    {
        return GetEffectValueColor(value) + value.ToString();
    }

    protected string ValueToStringPercent(float value)
    {
        return ValueToString(value) + "%";
    }
}
