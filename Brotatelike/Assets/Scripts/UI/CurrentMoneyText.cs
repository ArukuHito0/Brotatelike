using TMPro;
using UnityEngine;

public class CurrentMoneyText : MonoBehaviour
{
    private TextMeshProUGUI currentMoneyText;

    private void OnEnable()
    {
        Wallet.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        Wallet.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Awake()
    {
        currentMoneyText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateMoneyText(long money)
    {
        currentMoneyText.text = $"<sprite=8> {money}";
    }
}
