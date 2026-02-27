using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

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

    private bool isLocked = false;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
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
        isLocked = false;

        itemCard.SetActive(false);
    }
}
