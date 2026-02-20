using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private List<Canvas> canvasList = new List<Canvas>();
    private Dictionary<string, Canvas> canvasDic = new Dictionary<string, Canvas>();

    private void OnDestroy()
    {
        UpgradeCard.OnChooseUpgrade -= SetCanvasEnabled;
        FindObjectOfType<ExpComponent>().OnGetUpgrade -= SetCanvasEnabled;
    }

    private void Awake()
    {
        SetupDictionary();

        UpgradeCard.OnChooseUpgrade += SetCanvasEnabled;
        FindObjectOfType<ExpComponent>().OnGetUpgrade += SetCanvasEnabled;
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
