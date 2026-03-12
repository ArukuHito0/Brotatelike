using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCard : MonoBehaviour
{
    public IProduct product {  get; private set; }

    [SerializeField] private Sprite lockImage;
    [SerializeField] private Sprite unlockImage;

    [SerializeField] private GameObject itemCard;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private TextMeshProUGUI lockText;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemEffect;
    [SerializeField] private TextMeshProUGUI itemPrice;

    public bool isPayied { get; private set; } = false;
    public bool isLocked { get; private set; } = false;

    public void Initialize()
    {
        gameObject.SetActive(true);

        isPayied = false;

        itemFrame.color = product.Tier.GetTierColor();
        itemIcon.sprite = product.Icon;
        itemName.text = product.Name;
        itemEffect.text = product.GetDescriptionText();
        itemPrice.text = product.Price.ToString() + " G";
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void SetItemLock()
    {
        if (isPayied) return;

        isLocked = !isLocked;
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void Setproduct(IProduct data)
    {
        if (isLocked) return;

        this.product = data;
    }

    public void PayProduct()
    {
        product.PayProduct();

        isPayied = true;
        isLocked = false;

        itemCard.SetActive(false);
    }
}
