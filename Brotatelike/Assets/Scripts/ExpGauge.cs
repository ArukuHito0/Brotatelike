using TMPro;
using UnityEngine;

public class ExpGauge : GaugeUIBar
{
    [SerializeField]
    private ExpComponent expComponent;
    [SerializeField]
    private TextMeshProUGUI currentLevelText;

    private void OnDestroy()
    {
        expComponent.OnExpChanged -= UpdateFillAmount;
    }

    private void Awake()
    {
        expComponent.OnExpChanged += UpdateFillAmount;
    }

    public override void UpdateFillAmount(float rate)
    {
        base.UpdateFillAmount(rate);
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        currentLevelText.text = $"Lv.<size=40>{expComponent.CurrentLevel}";
    }
}
