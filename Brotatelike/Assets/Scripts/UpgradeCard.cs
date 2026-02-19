using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Transform upgradeCard;
    private Vector3 defalutScale;

    [SerializeField]
    private float scaleUpValue;

    public static event Action<string, bool> OnChooseUpgrade;

    private void Awake()
    {
        upgradeCard = this.transform;
        defalutScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 pickupScale = new Vector3(transform.localScale.x * scaleUpValue, transform.localScale.y * scaleUpValue);
        upgradeCard.localScale = pickupScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeCard.localScale = defalutScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnChooseUpgrade?.Invoke("UpgradeUI", false);
        OnChooseUpgrade?.Invoke("PlayerAndBulletStatusUI", false);

        TimeManager.SetTimeMode(TimeManager.TimeMode.Normal);
    }
}
