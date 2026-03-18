using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private List<Canvas> canvasList = new List<Canvas>();
    private Dictionary<string, Canvas> canvasDic = new Dictionary<string, Canvas>();

    private void OpenShopUI()
    {
        SetCanvasEnabled("ShopUI", true);
        SetCanvasEnabled("StatusUI", true);
    }

    private void CloseShopUI()
    {
        SetCanvasEnabled("ShopUI", false);
        SetCanvasEnabled("StatusUI", false);
    }

    private void OnDisable()
    {
        UpgradeCard.OnCloseUpgrade -= SetCanvasEnabled;
        if(PlayerController.Instance != null)
            PlayerController.Instance.ExpComponent.OnOpenUpgrade -= SetCanvasEnabled;
    }

    private void Awake()
    {
        SetupDictionary();

        UpgradeCard.OnCloseUpgrade += SetCanvasEnabled;
        if (PlayerController.Instance != null)
            PlayerController.Instance.ExpComponent.OnOpenUpgrade += SetCanvasEnabled;
    }

    private void SetupDictionary()
    {
        foreach (var canvas in canvasList)
        {
            if (canvas != null && !canvasDic.ContainsKey(canvas.name))
                canvasDic.Add(canvas.name, canvas);
        }
    }

    private Canvas GetCanvas(string name)
    {
        return canvasDic.TryGetValue(name, out Canvas canvas) ? canvas : null;
    }

    private void SetCanvasEnabled(string name, bool isActive)
    {
        var canvas = GetCanvas(name);
        if (canvas != null)
        {
            canvas.enabled = isActive;
        }
    }
}
