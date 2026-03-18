using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductPanel : MonoBehaviour
{
    [SerializeField] private Image iconBackground;
    [SerializeField] private Image productFrame;
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI productEffect;

    public void UpdatePanelVisual(IProduct product)
    {
        Color color = product.Tier.GetTierColor();
        iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
        productFrame.color = color;
        productIcon.sprite = product.Icon;
        productName.text = product.Name;
        productEffect.text = product.GetDescriptionText();
    }
}
