using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] int ammoDamage = 10;
    [SerializeField] int maxClip = 10;
    private int inClip;

    void Start()
    {
        inClip = maxClip;
    }
    public int ammoCount()
    {
        Debug.Log("current Ammo: " + ammoAmount.ToString());
        return ammoAmount;
    }

    public void dropAmmo()
    {
        inClip--;
    }

    public int damageFromAmmo()
    {
        return ammoDamage;
    }

    public void addClip()
    {
        ammoAmount += maxClip;
    }

    public int clipCount()
    {
        return inClip;
    }

    public void reload()
    {
        if(maxClip > ammoAmount)
        {
            inClip = ammoAmount;
            ammoAmount = 0;
        } else
        {
            ammoAmount -= maxClip;
            inClip = maxClip;
        }
    }
}
