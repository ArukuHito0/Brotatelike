using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static Action<float> OnUpdateHealthBar;

    private Image healthBar;

    private void Awake()
    {
        healthBar = GetComponent<Image>();

        OnUpdateHealthBar += UpdateHealthBar;
    }

    private void UpdateHealthBar(float healthRate)
    {
        healthBar.fillAmount = healthRate;
    }
}
