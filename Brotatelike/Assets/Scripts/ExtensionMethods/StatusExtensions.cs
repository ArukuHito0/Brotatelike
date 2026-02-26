using UnityEngine;
using System;

public enum UpgradeStatus
{
    MaxHealth,
    Strength,
    AttackSpeed,
    Critical,
    AttackRange,
    MoveSpeed,
    Armor,
    CollectRange,
    DodgeChance,
    Luck,
}

public static class StatusExtensions
{
    public static void ApplyStatusUP(this UpgradeStatus status, int amount) => status.GetIncreaseMethod()?.Invoke(amount);

    public static Action<int> GetIncreaseMethod(this UpgradeStatus status) => status switch
    {
        UpgradeStatus.MaxHealth => PlayerRuntimeStatus.Instance.AddMaxHealth,
        UpgradeStatus.Strength => PlayerRuntimeStatus.Instance.AddStrength,
        UpgradeStatus.AttackSpeed => PlayerRuntimeStatus.Instance.AddAttackSpeed,
        UpgradeStatus.Critical => PlayerRuntimeStatus.Instance.AddCritical,
        UpgradeStatus.AttackRange => PlayerRuntimeStatus.Instance.AddAttackRange,
        UpgradeStatus.MoveSpeed => PlayerRuntimeStatus.Instance.AddMoveSpeed,
        UpgradeStatus.Armor => PlayerRuntimeStatus.Instance.AddArmor,
        UpgradeStatus.CollectRange => PlayerRuntimeStatus.Instance.AddCollectRange,
        UpgradeStatus.DodgeChance => PlayerRuntimeStatus.Instance.AddDodgeChance,
        UpgradeStatus.Luck => PlayerRuntimeStatus.Instance.AddLuck,
        _ => null,
    };

    public static string GetUpgradeStatusName(this UpgradeStatus status) => status switch
    {
        UpgradeStatus.MaxHealth => "Å‘åHP",
        UpgradeStatus.Strength => "UŒ‚—Í",
        UpgradeStatus.AttackSpeed => "UŒ‚‘¬“x(%)",
        UpgradeStatus.Critical => "¸ØÃ¨¶Ù—¦(%)",
        UpgradeStatus.AttackRange => "UŒ‚”ÍˆÍ",
        UpgradeStatus.MoveSpeed => "ˆÚ“®‘¬“x",
        UpgradeStatus.Armor => "ƒA[ƒ}[",
        UpgradeStatus.CollectRange => "‰ñŽû”ÍˆÍ",
        UpgradeStatus.DodgeChance => "‰ñ”ð—¦(%)",
        UpgradeStatus.Luck => "‰^",
        _ => null,
    };
}
