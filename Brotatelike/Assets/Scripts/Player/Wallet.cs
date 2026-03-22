using System;
using UnityEngine;

[System.Serializable]
public class Wallet
{
    [SerializeField]
    // Š‹à
    private long currentMoney;
    public long CurrentMoney => currentMoney;

    // Š‹àÅ‘å’l
    public static readonly long MAX_HOLD_MONEY = 999999999999;

    public static event Action<long> OnMoneyChanged;

    public void AddMoney(long amount)
    {
        currentMoney += amount;
        
        if (currentMoney > MAX_HOLD_MONEY)
        {
            currentMoney = MAX_HOLD_MONEY;
        }

        OnMoneyChanged?.Invoke(currentMoney);
    }

    public void RemoveMoney(long amount)
    {
        currentMoney -= amount;
        
        if (currentMoney < 0)
        {
            currentMoney = 0;
        }

        OnMoneyChanged?.Invoke(currentMoney);
    }

    public bool CanBuy(long amount)
    {
        return currentMoney >= amount;
    }
}