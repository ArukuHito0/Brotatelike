using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductPanel : MonoBehaviour
{
    public IProduct product {  get; private set; }

    [SerializeField] private Image productFrame;
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI productEffect;

    public bool isPayied { get; private set; } = false;
    public bool isLocked { get; private set; } = false;

    public void Initialize()
    {
        gameObject.SetActive(true);

        isPayied = false;

        UpdateCardVisual();
    }

    public void UpdateCardVisual()
    {
        productFrame.color = product.Tier.GetTierColor();
        productIcon.sprite = product.Icon;
        productName.text = product.Name;
        productEffect.text = product.GetDescriptionText();
    }

    public void SetproductLock()
    {
        if (isPayied) return;

        isLocked = !isLocked;
    }

    public void SetProduct(IProduct data)
    {
        if (isLocked) return;

        this.product = data;
    }

    public void PayProduct()
    {
        product?.PayProduct();

        isPayied = true;
        isLocked = false;

        gameObject.SetActive(false);
    }
}
