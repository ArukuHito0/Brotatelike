using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [Serializable]
    public struct UpgradeStats
    {
        public PlayerStatus status;
        public int value;
    }

    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;
    [SerializeField] private TierType itemTier;
    [SerializeField] private uint itemPrice;
    [SerializeField] private UpgradeStats[] stats;

    public Sprite ItemIcon => itemIcon;
    public string ItemName => itemName;
    public TierType ItemTier => itemTier;
    public uint ItemPrice => itemPrice;
    public UpgradeStats[] Stats => stats;
}
