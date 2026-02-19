using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GaugeUIBar : MonoBehaviour
{
    [SerializeField]
    private Image gaugeImage;

    public void UpdateFillAmount(float rate)
    {
        gaugeImage.fillAmount = rate;
    }
}
