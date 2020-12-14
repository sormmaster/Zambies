using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSC;
    [SerializeField] float range = 100f;
    [SerializeField] int baseDamage = 15;
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] ParticleSystem rangeIndicator;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] bool isActive = true;
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] float delayOnShot = 0f;

    private float lastShot;
    private float ReloadingTill;
    

    void Start()
    {
        lastShot = Time.time;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && isActive)
        {
            if(Time.time > lastShot && Time.time > ReloadingTill)
            {
                Shoot();
            }
            
        } else if(Input.GetKeyDown("r") && isActive)
        {
            Reload();
        } else if (Input.GetKeyDown("f"))
        {
            castForRange();
        }
    }

    private void Shoot()
    {
        lastShot = Time.time + delayOnShot;
        if(ammoSlot.clipCount() > 0)
        {
            ammoSlot.dropAmmo();
            PlayMuzzleFlash();
            CastToHit();
            StartCoroutine(FindObjectOfType<DeathHandler>().notifyUser("Waiting", delayOnShot));
        } else
        {
            Reload();
        }
    }

    private void PlayMuzzleFlash()
    {
        flash.Play();
    }

    private void Reload()
    {
        StartCoroutine(FindObjectOfType<DeathHandler>().notifyUser("Reloading", reloadDelay));
        ReloadingTill = Time.time + reloadDelay;
        ammoSlot.reload();
    }

    private void castForRange()
    {
        RaycastHit check;
        if (Physics.Raycast(FPSC.transform.position, FPSC.transform.forward, out check, range))
        {
            CreateImpact(check);
            rangeIndicator.transform.position = check.point;
            rangeIndicator.Play();
        }
    }
    private void CastToHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSC.transform.position, FPSC.transform.forward, out hit, range))
        {
            CreateImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target)
            {
                if (target.gotHit(baseDamage + ammoSlot.damageFromAmmo(), transform.position))
                {
                    ammoSlot.addClip();
                }
            }
        }
    }

    private void CreateImpact(RaycastHit hit)
    {
       GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.2f);

    }

    public Ammo getAmmoSlot()
    {
        return ammoSlot;
    }
}
