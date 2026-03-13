using System;
using UnityEngine;

public enum TierType
{
    Common,
    Rare,
    Super,
    Legend,
    Unique,
}

public static class TierExtensions
{
    // TierType•Ï”.GetTierColor‚Æ‚¢‚¤•—‚ÉŒÄ‚×‚é
    public static Color GetTierColor(this TierType tier) => tier switch
    {
        TierType.Common => Color.white,
        TierType.Rare => Color.skyBlue,
        TierType.Super => Color.mediumOrchid,
        TierType.Legend => Color.gold,
        TierType.Unique => Color.softRed,
        _ => Color.white,
    };

    public static TierType TierUp(this TierType tier)
    {
        if(tier == TierType.Unique) return tier;
        return tier + 1;
    }
}
