using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatus", menuName = "PlayerStatus")]
public class PlayerStatusData : ScriptableObject
{
    public static PlayerStatusData Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }

    private void OnDisable()
    {
        if (Instance != null) Instance = null;
    }

    [SerializeField] private float baseMaxHealth;
    [SerializeField] private float baseStrength;
    [SerializeField] private float baseAttackSpeed;
    [SerializeField] private float baseCritical;
    [SerializeField] private float baseAttackRange;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseArmor;
    [SerializeField] private float baseCollectRange;
    [SerializeField] private float baseDodgeChance;
    [SerializeField] private int baseLuck;

    public float BaseMaxHealth => baseMaxHealth;
    public float BaseStrength => baseStrength;
    public float BaseAttackSpeed => baseAttackSpeed;
    public float BaseCritical => baseCritical;
    public float BaseAttackRange => baseAttackRange;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float BaseArmor => baseArmor;
    public float BaseCollectRange => baseCollectRange;
    public float BaseDodgeChance => baseDodgeChance;
    public int BaseLuck => baseLuck;
}