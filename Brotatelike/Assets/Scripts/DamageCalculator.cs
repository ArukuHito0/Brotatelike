using System;
using UnityEngine;

public static class DamageCalculator
{
    public static event Action<float> OnCalculateDamage;

    public static float CalculateDamage(float baseDamage, float criticalChance, float criticalMultiplier)
    {
        float damage = baseDamage;

        if (criticalChance > 0)
        {
            if (UnityEngine.Random.value <= criticalChance)
            {
                damage *= criticalMultiplier;
            }
        }            

        OnCalculateDamage?.Invoke(damage);

        return damage;
    }
}
