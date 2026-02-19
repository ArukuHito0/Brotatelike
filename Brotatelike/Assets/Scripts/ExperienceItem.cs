using System;
using UnityEngine;

public class ExperienceItem : PooledObject, IPickable
{
    [SerializeField]
    private int experiencePoint;

    public void PickUp(ExpComponent expComponent)
    {
        expComponent.AddExp(experiencePoint);

        Release();
    }
}
