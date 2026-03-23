using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCard : MonoBehaviour
{
    public IProduct product {  get; private set; }

    [SerializeField] private Sprite lockImage;
    [SerializeField] private Sprite unlockImage;

    [SerializeField] private Image iconBackground;
    [SerializeField] private Image productFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private TextMeshProUGUI lockText;
    [SerializeField] private Image productIcon;
    [SerializeField] private TextMeshProUGUI productName;
    [SerializeField] private TextMeshProUGUI productEffect;
    [SerializeField] private Image priceLabel;
    [SerializeField] private TextMeshProUGUI productPrice;

    public bool isPayied { get; private set; } = false;
    public bool isLocked { get; private set; } = false;

    private void OnEnable()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged += UpdateCardVisual;
    }

    private void OnDisable()
    {
        PlayerController.Instance.playerRuntimeStatus.OnStatusChanged -= UpdateCardVisual;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);

        isPayied = false;

        UpdateCardVisual();
    }

    public void UpdateCardVisual()
    {
        Color color = product.Tier.GetTierColor();
        iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
        productFrame.color = color;
        productIcon.sprite = product.Icon;
        productName.text = product.Name;
        productEffect.text = product.GetDescriptionText();
        productPrice.text = product.Price.ToString();
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void SetProductLock()
    {
        if (isPayied) return;

        isLocked = !isLocked;
        lockIcon.sprite = isLocked ? lockImage : unlockImage;
        lockText.text = isLocked ? "ロック : ON" : "ロック : OFF";
    }

    public void SetProduct(IProduct data)
    {
        if (isLocked) return;

        this.product = data;
    }

    public void PayProduct()
    {
        if (!product.CanBuy())
        {
            Debug.Log("購入条件を満たしていません");
            return;
        }

        PlayerController.Instance.wallet.RemoveMoney(product.Price);

        product?.PayProduct();

        isPayied = true;
        isLocked = false;

        priceLabel.rectTransform.localScale = new Vector3(1, 1, 1);

        gameObject.SetActive(false);
    }
}
