using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    public ItemData itemData {  get; private set; }

    [SerializeField] private Sprite lockImage;
    [SerializeField] private Sprite unlockImage;

    [SerializeField] private GameObject itemCard;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemEffect;
    [SerializeField] private TextMeshProUGUI itemEffectValue;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public bool isPayied { get; private set; } = false;
    public bool isLocked { get; private set; } = false;

    public event Action<ItemData> OnPayItem;

    public void Initialize()
    {
        gameObject.SetActive(true);

        isPayied = false;

        itemFrame.color = itemData.tier.GetTierColor();
        itemIcon.sprite = itemData.itemIcon;
        itemName.text = itemData.itemName;
        itemEffect.text = itemData.GetUpgradeName();
        itemEffectValue.text = itemData.GetUpgradeValueText();
        itemPrice.text = itemData.itemPrice.ToString() + " G";
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
    }

    public void SetItemLock()
    {
        if (isPayied) return;

        isLocked = !isLocked;
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
    }

    public void SetItemData(ItemData item)
    {
        if (isLocked) return;

        this.itemData = item;
    }

    public void PayItem()
    {
        itemData.Upgrade();
        itemPrice.text = "Sold Out !";

        isPayied = true;
        isLocked = false;
        lockIcon.sprite = unlockImage;

        OnPayItem?.Invoke(itemData);

        itemCard.SetActive(false);
    }
}
