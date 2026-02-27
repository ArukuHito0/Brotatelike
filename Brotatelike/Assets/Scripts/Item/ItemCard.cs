using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private UpgradeData upgrade;

    [SerializeField] private GameObject itemCard;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemEffect;
    [SerializeField] private TextMeshProUGUI itemEffectValue;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private void OnEnable()
    {
        itemFrame.color = upgrade.tier.GetTierColor();
        itemIcon.sprite = upgrade.upgradeIcon;
        itemName.text = upgrade.upgradeName;
        itemEffect.text = upgrade.GetUpgradeName();
        itemEffectValue.text = upgrade.GetUpgradeValueText();
    }

    private void Awake()
    {

    }

    public void PayItem()
    {
        upgrade.Upgrade();

        itemCard.SetActive(false);
    }
}
