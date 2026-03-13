using System;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "WeaponData")]
public class WeaponData : ScriptableObject, IProduct
{
    [Serializable]
    public struct DamageMultiplier
    {
        public PlayerStatus status;
        [Range(0, 100)] public int Chance;
    }

    [Header("ステータス")]
    [SerializeField] private Sprite weaponIcon;
    [SerializeField] private string weaponName;
    [SerializeField] private TierType weaponTier;
    [SerializeField] private uint weaponPrice;

    [SerializeField] private float baseDamage;
    public DamageMultiplier damageMultiplier;
    [SerializeField] private float baseCriticalChance;
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

    // 計算・表示に使用するプロパティ
    public float Damage => baseDamage;
    public float CriticalChance => baseCriticalChance;
    public float Range => baseRange;
    public float CoolTime => baseCoolTime;
    public float baseAngle => Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;    // 攻撃を発射するデフォルトの向き
    public float fireRate => cycleTime / bulletCnt;                                             // 連続して弾を発射するときの間隔

    #region IProductのプロパティ
    public TierType Tier => weaponTier;
    public  Sprite Icon => weaponIcon;
    public  string Name => weaponName;
    public  uint Price => weaponPrice;

    public  void PayProduct()
    {

    }

    public  string GetDescriptionText()
    {
        string text = string.Empty;
        text = $"ダメージ: {Damage}\n" +
               $"クリティカル率: {CriticalChance}%\n" +
               $"クリティカルダメージ: x{baseCriticalDamageMultiplier}\n" +
               $"クールタイム: {CoolTime}s\n" +
               $"射程距離: {Range}m\n";

        return text;
    }
    #endregion
}
