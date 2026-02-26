using System;
using TMPro;
using UnityEngine;

public class HealthGauge : GaugeUIBar
{
    [SerializeField]
    private HealthComponent healthComponent;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private void OnDestroy()
    {
        healthComponent.OnHealthChanged -= UpdateFillAmount;
        PlayerRuntimeStatus.Instance.OnMaxHealthUpdate -= UpdateFillAmount;
    }

    private void Awake()
    {
        healthComponent.OnHealthChanged += UpdateFillAmount;
        PlayerRuntimeStatus.Instance.OnMaxHealthUpdate += UpdateFillAmount;
    }

    public override void UpdateFillAmount(float rate)
    {
        base.UpdateFillAmount(rate);

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"{healthComponent.CurrentHealth} <size=25>/ {PlayerRuntimeStatus.Instance?.MaxHealth}";
    }
}
