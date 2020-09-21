using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] int ammoDamage = 10;
    [SerializeField] int maxClip = 10;

    public int ammoCount()
    {
        Debug.Log("current Ammo: " + ammoAmount.ToString());
        return ammoAmount;
    }

    public void dropAmmo()
    {
        ammoAmount--;
    }

    public int damageFromAmmo()
    {
        return ammoDamage;
    }

    public void addClip()
    {
        ammoAmount += maxClip;
    }
}
