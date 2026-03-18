using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    public WeaponData weaponData {  get; private set; }

    [SerializeField] private Image weaponIcon;
    [SerializeField] private Image iconBackground;
    [SerializeField] private Image weaponFrame;

    [SerializeField] private ProductPanel productPanel;

    public void Initialize(WeaponData data)
    {
        if (data == null)
        {
            weaponIcon.enabled = false;
        }
        else
        {
            weaponIcon.enabled = true;
            weaponData = data;
            weaponIcon.sprite = data.Icon;
            Color color = weaponData.Tier.GetTierColor();
            iconBackground.color = new Color(color.r, color.g, color.b, 0.078f);
            weaponFrame.color = color;
        }
    }

    public void OpenWeaponPanel()
    {
        if (weaponData == null) return;

        productPanel.GetComponent<CanvasGroup>().alpha = 1;

        productPanel.UpdatePanelVisual(weaponData);
    }

    public void CloseWeaponPanel()
    {
        if (weaponData == null) return;

        productPanel.GetComponent<CanvasGroup>().alpha = 0;
    }
}
