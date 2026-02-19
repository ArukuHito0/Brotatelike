using System;
using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;

    public float healthRate
    {
        get
        {
            return (float)currentHealth / maxHealth;
        }
    }

    public bool IsDead => currentHealth <= 0;

    public event Action<float> OnHealthChanged;
    public event Action OnDead;

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(healthRate);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= (int)damage;

        if (IsDead)
        {
            Dead();
        }

        OnHealthChanged?.Invoke(healthRate);
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + (int)amount, 0, maxHealth);

        OnHealthChanged?.Invoke(healthRate);
    }

    private void Dead()
    {
        OnHealthChanged?.Invoke(0);
        OnDead?.Invoke();
    }
}
