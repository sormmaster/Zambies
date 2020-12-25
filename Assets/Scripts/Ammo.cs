using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] int ammoDamage = 10;
    [SerializeField] int maxClip = 10;
    [SerializeField] public AmmoType type;
    private int inClip;

    void Start()
    {
        inClip = maxClip;
    }
    public int ammoCount()
    {
        return ammoAmount;
    }

    public void dropAmmo()
    {
        inClip--;
        FindObjectOfType<WeaponManager>().setAmmoText(inClip, ammoAmount);
    }

    public int damageFromAmmo()
    {
        return ammoDamage;
    }

    public void addBullets(int count)
    {
        ammoAmount += count;
        FindObjectOfType<WeaponManager>().setAmmoText(inClip, ammoAmount);
    }
    public void addClip()
    {
        ammoAmount += maxClip;
        FindObjectOfType<WeaponManager>().setAmmoText(inClip, ammoAmount);
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
        FindObjectOfType<WeaponManager>().setAmmoText(inClip, ammoAmount);
    }
}
