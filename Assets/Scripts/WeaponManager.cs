using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] Text ammo;
    void Start()
    {
        SetActiveWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;
        proessScrollWheel();
        ProcessKeyInput();
        if(previousWeapon != currentWeapon)
        {
            SetActiveWeapon();
        }
    }

    void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    void proessScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            } else
            {
                currentWeapon++;
            }
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void SetActiveWeapon()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform) {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
                findCurrentAmmo(weapon);
                    
            } else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }

    public bool gotAmmo(AmmoType type, int ammount)
    {
        var ammoSlots = GetComponentsInChildren<Ammo>();
        foreach (Ammo check in ammoSlots)
        {
            if(check.type == type)
            {
                check.addBullets(ammount);
                return true;
            }
        }
        return false;
    }

    public void findCurrentAmmo(Weapon weapon)
    {
        Ammo tempAmmo = weapon.getAmmoSlot();
        setAmmoText(tempAmmo.clipCount(), tempAmmo.ammoCount());
    }

    public void findCurrentAmmo(Transform weapon)
    {
        Ammo tempAmmo = weapon.GetComponent<Weapon>().getAmmoSlot();
        setAmmoText(tempAmmo.clipCount(), tempAmmo.ammoCount());

    }

    public void setAmmoText(int inClip, int inStock)
    {
        string toSet = "Ammo: " + inClip.ToString() + "/" + inStock.ToString();
        ammo.text = toSet;
    }
}
