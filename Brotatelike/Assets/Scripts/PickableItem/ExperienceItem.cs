using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceItem : PickableItem
{
    [SerializeField] private int experiencePoint;

    protected override void OnPickUpItem()
    {
        PlayerController.Instance.ExpComponent.AddExp(experiencePoint);
    }
}
