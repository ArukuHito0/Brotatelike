using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private GameObject itemCard;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Image lockIcon;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemEffect;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private void OnEnable()
    {
        
    }

    private void Awake()
    {

    }

    public void PayItem()
    {
        itemCard.SetActive(false);
    }
}
