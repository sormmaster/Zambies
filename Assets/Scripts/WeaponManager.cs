using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;

    void Start()
    {
        SetActiveWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetActiveWeapon()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform) {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            } else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
