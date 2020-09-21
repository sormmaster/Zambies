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
    [SerializeField] Ammo ammoSlot;
    [SerializeField] bool isActive = true;

    [SerializeField] float delayOnShot = 0f;

    private float lastShot;

    

    void Start()
    {
        lastShot = Time.time;
    }

    void Update()
    {
        if (!isActive || Time.time - lastShot < delayOnShot)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        lastShot = Time.time;
        if(ammoSlot.ammoCount() > 0)
        {
            ammoSlot.dropAmmo();
            PlayMuzzleFlash();
            CastToHit();
        }
    }

    private void PlayMuzzleFlash()
    {
        flash.Play();
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
}
