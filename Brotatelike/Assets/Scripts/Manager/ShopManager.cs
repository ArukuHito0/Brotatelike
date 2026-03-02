using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Dictionary<ItemData, bool> itemDatas;
    [SerializeField] private ItemCard[] items;

    public event Action OnEndShopping;

    private void OnEnable()
    {
        FindObjectOfType<EnemyGenerator>().OnEndWave += UpdateItems;
    }

    private void OnDisable()
    {
        FindObjectOfType<EnemyGenerator>().OnEndWave -= UpdateItems;
    }

    public void UpdateItems()
    {

    }

    public void GoToNextWave()
    {
        OnEndShopping?.Invoke();
    }
}
