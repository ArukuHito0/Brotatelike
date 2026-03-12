using UnityEngine;

public static class ValueColorExtentions
{
    public static string ToColorText(this int value)
    {
        return value > 0 ? $"<color=green>+{value}</color>" : $"<color=red>{value}</color>";
    }

    public static string ToColorText(this float value)
    {
        return value > 0 ? $"<color=green>+{value}</color>" : $"<color=red>{value}</color>";
    }
}
