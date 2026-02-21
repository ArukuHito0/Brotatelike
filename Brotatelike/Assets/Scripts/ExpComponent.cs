using System;
using System.Threading.Tasks;
using UnityEngine;

public class ExpComponent : MonoBehaviour
{
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;
    private int exp = 0;
    private int levelUpExp = 100;
    public float expRate
    {
        get
        {
            return (float)exp / levelUpExp;
        }
    }

    public event Action<float> OnExpChanged;
    public event Action OnLevelChanged;
    public event Action<string, bool> OnOpenUpgrade;

    private void Awake()
    {
        OnExpChanged?.Invoke(0);
    }

    public void AddExp(int amount)
    {
        exp += amount;

        if (exp >= levelUpExp)
        {
            var e = exp - levelUpExp;
            exp = e <= 0 ? 0 : e;
            LevelUp();

            if (exp != 0)
            {
                AddExp(exp);
            }
        }

        OnExpChanged?.Invoke(expRate);
    }

    public void LevelUp()
    {
        currentLevel++;

        OnLevelChanged?.Invoke();
        OnOpenUpgrade?.Invoke("UpgradeUI", true);
        OnOpenUpgrade?.Invoke("StatusUI", true);

        TimeManager.SetTimeMode(TimeManager.TimeMode.Pause);
    }
}
