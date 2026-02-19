using System;
using UnityEngine;

public class ExperienceItem : PooledObject, IPickable
{
    [SerializeField]
    private int experiencePoint;

    public void PickUp()
    {
        PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.AddExp(experiencePoint);

        Release();
    }
}
