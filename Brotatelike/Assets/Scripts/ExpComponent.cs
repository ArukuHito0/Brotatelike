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
    public static event Action<string, bool> OnLevelUp;

    private void Awake()
    {
        OnExpChanged?.Invoke(expRate);
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

        OnLevelUp?.Invoke("UpgradeUI", true);
        OnLevelUp?.Invoke("PlayerAndBulletStatusUI", true);

        TimeManager.SetTimeMode(TimeManager.TimeMode.Pause);
    }
}
