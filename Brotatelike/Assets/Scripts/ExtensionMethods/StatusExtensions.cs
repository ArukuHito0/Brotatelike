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
        UpgradeStatus.MaxHealth => PlayerController.Instance.playerRuntimeStatus.AddMaxHealth,
        UpgradeStatus.Strength => PlayerController.Instance.playerRuntimeStatus.AddStrength,
        UpgradeStatus.AttackSpeed => PlayerController.Instance.playerRuntimeStatus.AddAttackSpeed,
        UpgradeStatus.Critical => PlayerController.Instance.playerRuntimeStatus.AddCritical,
        UpgradeStatus.AttackRange => PlayerController.Instance.playerRuntimeStatus.AddAttackRange,
        UpgradeStatus.MoveSpeed => PlayerController.Instance.playerRuntimeStatus.AddMoveSpeed,
        UpgradeStatus.Armor => PlayerController.Instance.playerRuntimeStatus.AddArmor,
        UpgradeStatus.CollectRange => PlayerController.Instance.playerRuntimeStatus.AddCollectRange,
        UpgradeStatus.DodgeChance => PlayerController.Instance.playerRuntimeStatus.AddDodgeChance,
        UpgradeStatus.Luck => PlayerController.Instance.playerRuntimeStatus.AddLuck,
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
