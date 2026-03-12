using UnityEngine;

public static class ProductFactory
{
    public static IProduct Create(ItemData itemData) => new ItemOfProduct { data = itemData };
    public static IProduct Create(WeaponData weaponData) => new WeaponOfProduct { data = weaponData };
}
