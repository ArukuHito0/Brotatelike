using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class ShopCurator : MonoBehaviour
{
    [SerializeField] private ItemLottery itemLottery;
    [SerializeField] private WeaponLottery weaponLottery;

    [SerializeField] private uint weaponSpawnRate;
    private float WeaponSpawnRate => weaponSpawnRate * 0.01f;

    [SerializeField] private ProductCard[] productCards;

    private HashSet<IProduct> lineups = new HashSet<IProduct>();

    private void Awake()
    {
        itemLottery.LoadAllAssets();
        weaponLottery.LoadAllAssets();
    }

    // 全商品を抽選
    public void UpdateProducts()
    {
        lineups.Clear();

        // ロック済みの商品のデータを追加
        foreach(var product in productCards)
            if(product.isLocked)
                lineups.Add(product.product);

        if (productCards == null) return;

        foreach (var product in productCards)
        {
            if(product.isLocked) continue;

            IProduct data = null;

            // 商品が抽選されるまでループ
            while (data == null)
            {
                // 武器かアイテムを抽選
                IProduct candidate = Random.value < WeaponSpawnRate ? weaponLottery.GetProduct() : itemLottery.GetProduct();
                 
                // 既に出ていない商品なら代入
                if(!lineups.Contains(candidate))
                {
                    data = candidate;

                    // ラインナップに追加し、カードに商品データをセット
                    lineups.Add(data);

                    product?.SetProduct(data);
                    product?.Initialize();

                    break;
                }
                else
                {
                    data = null;
                }
            }
            
        }
    }
}
