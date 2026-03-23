using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLvText;
    [SerializeField] private TextMeshProUGUI statusNameText;
    [SerializeField] private TextMeshProUGUI statusValueText;

    private void OnDestroy()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged -= MainStatusDisplay;
    }

    private void Start()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged += MainStatusDisplay;

        MainStatusDisplay();
    }

    private void MainStatusDisplay()
    {
        currentLvText.text = $"Lv.<size=50>{PlayerController.Instance.ExpComponent.CurrentLevel}";

        statusNameText.text = PlayerStatus.MaxHealth.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Strength.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackSpeed.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Critical.GetPlayerStatusName() + "\n" +
                              PlayerStatus.AttackRange.GetPlayerStatusName() + "\n" +
                              PlayerStatus.MoveSpeed.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Armor.GetPlayerStatusName() + "\n" +
                              PlayerStatus.DodgeChance.GetPlayerStatusName() + "\n" +
                              PlayerStatus.Luck.GetPlayerStatusName();

        statusValueText.text = PlayerStatus.MaxHealth.GetRuntimeStatus() + $" | {StatToColorText(PlayerStatus.MaxHealth)}" + "\n" +
                               StatToColorText(PlayerStatus.Strength) + "\n" +
                               StatToColorText(PlayerStatus.AttackSpeed) + "\n" +
                               StatToColorText(PlayerStatus.Critical) + "\n" +
                               StatToColorText(PlayerStatus.AttackRange) + "\n" +
                               StatToColorText(PlayerStatus.MoveSpeed) + "\n" +
                               StatToColorText(PlayerStatus.Armor) + "\n" +
                               StatToColorText(PlayerStatus.DodgeChance) + "\n" +
                               StatToColorText(PlayerStatus.Luck);
    }

    public string StatToColorText(PlayerStatus status)
    {
        float baseStatus = status.GetBaseStatus();
        float bonusStatus = status.GetBonusStatus();

        if(bonusStatus == 0)
            return $"{bonusStatus.ToString("F0")}";
        else if (bonusStatus > 0)
            return $"<color=green>{bonusStatus.ToString("F0")}</color>";
        else
            return $"<color=red>{bonusStatus.ToString("F0")}</color>";
    }
}
