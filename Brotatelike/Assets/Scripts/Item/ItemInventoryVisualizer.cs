using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject iteminventoryContent;
    [SerializeField] private ProductIcon productIconPrefab;

    private Dictionary<ItemData, ProductIcon> productIconDict = new Dictionary<ItemData, ProductIcon>();

    private void Start()
    {
        PlayerController.Instance.ItemInventory.OnItemAdded += VisualizeInventory;
    }

    private void OnDisable()
    {
        PlayerController.Instance.ItemInventory.OnItemAdded -= VisualizeInventory;
    }

    private void VisualizeInventory(ItemData data, int cnt)
    {
        if (PlayerController.Instance.ItemInventory.GetItemCnt(data) == 1)
        {
            ProductIcon icon = Instantiate(productIconPrefab, iteminventoryContent.transform);

            if (!productIconDict.ContainsKey(data))
            {
                productIconDict[data] = icon;
            }

            icon.Initialize(data, cnt);
        }
        else
        {
            productIconDict[data].Initialize(data, cnt);
        }
    }
}
