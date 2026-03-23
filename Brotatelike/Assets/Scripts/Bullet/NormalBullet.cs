using UnityEngine;

public class NormalBullet : BulletBase
{
    protected override void OnHit()
    {
        hitCache?.TakeDamage(DamageCalculator.CalculateDamage(weaponData.Damage, weaponData.CriticalChance, weaponData.CriticalMultiplier));
        Release();
    }
}
