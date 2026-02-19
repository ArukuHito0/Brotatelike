using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Transform upgradeCard;
    private Vector3 defalutScale;

    [SerializeField]
    private float scaleUpValue;

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
        
    }
}
