using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    [Serializable]
    public struct DamageMultiplier
    {
        public PlayerStatus status;
        [Range(0, 100)] public int rate;
    }

    [Header("ステータス")]
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private string weaponName;
    [SerializeField] private TierType weaponTier;
    [SerializeField] private uint weaponPrice;

    [SerializeField] private float baseDamage;
    public DamageMultiplier damageMultiplier;
    [SerializeField] private float baseCriticalRate;
    [SerializeField] private float baseCriticalDamageMultiplier;
    [SerializeField] private float baseRange;
    [SerializeField] private float baseCoolTime;
    
    [Header("パラメータ")]
    public int bulletCnt;
    public float bulletSpeed;
    public Vector2 fireDirection;
    [Range(0, 360)] public int spreadAngle;
    [Range(0, 100)] public int dispersion;
    public float cycleTime;
    public bool isTargetting;

    /// <summary>
    /// 参照用プロパティ
    /// </summary>

    // 商品棚用プロパティ
    public TierType WeaponTier => weaponTier;
    public Sprite WeaponIcon => weaponIcon;
    public string WeaponName => weaponName;
    public uint WeaponPrice => weaponPrice;

    // 計算・表示に使用するプロパティ
    public float Damage => baseDamage;
    public float CriticalRate => baseCriticalRate;
    public float Range => baseRange;
    public float CoolTime => baseCoolTime;
    public float baseAngle => Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;    // 攻撃を発射するデフォルトの向き
    public float fireRate => cycleTime / bulletCnt;                                             // 連続して弾を発射するときの間隔
}
