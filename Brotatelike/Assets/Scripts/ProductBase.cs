using UnityEngine;

public abstract class ProductBase : IProduct
{
    public abstract TierType Tier { get; }
    public abstract Sprite Icon { get; }
    public abstract string Name { get; }
    public abstract uint Price { get; }

    public abstract void PayProduct();
    public abstract string GetDescriptionText();
}
