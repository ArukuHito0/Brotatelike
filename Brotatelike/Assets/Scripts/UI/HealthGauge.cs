using System;
using TMPro;
using UnityEngine;

public class HealthGauge : GaugeUIBar
{
    [SerializeField]
    private HealthComponent healthComponent;
    [SerializeField]
    private TextMeshProUGUI healthText;

    private void OnDisable()
    {
        if(healthComponent != null)
            healthComponent.OnHealthChanged += UpdateFillAmount;
    }

    private void Awake()
    {
        if (healthComponent != null)
            healthComponent.OnHealthChanged += UpdateFillAmount;
    }

    public override void UpdateFillAmount(float rate)
    {
        base.UpdateFillAmount(rate);

        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"{healthComponent.currentHealth} <size=25>/ {healthComponent.maxHealth}";
    }
}
