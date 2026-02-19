using System.Collections.Generic;
using UnityEngine;
using System;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private List<Canvas> canvasList = new List<Canvas>();
    private Dictionary<string, Canvas> canvasDic = new Dictionary<string, Canvas>();

    public static event Action<string, bool> OnSetCanvasEnabled;

    private void OnDisable()
    {
        OnSetCanvasEnabled -= OnSetCanvasEnabled;
    }

    private void Awake()
    {
        SetupDictionary();
        OnSetCanvasEnabled += SetCanvasEnabled;
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

    public void SetCanvasEnabled(string name, bool isActive)
    {
        var canvas = GetCanvas(name);
        if (canvas != null)
        {
            canvas.enabled = isActive;
        }
    }
}
