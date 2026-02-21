using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private List<Canvas> canvasList = new List<Canvas>();
    private Dictionary<string, Canvas> canvasDic = new Dictionary<string, Canvas>();

    private void OnDestroy()
    {
        UpgradeCard.OnCloseUpgrade -= SetCanvasEnabled;
        player.ExpComponent.OnOpenUpgrade -= SetCanvasEnabled;
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();

        SetupDictionary();

        UpgradeCard.OnCloseUpgrade += SetCanvasEnabled;
        player.ExpComponent.OnOpenUpgrade += SetCanvasEnabled;
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
