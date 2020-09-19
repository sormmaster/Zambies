using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSC;
    [SerializeField] float range = 100f;
    [SerializeField] float baseDamage = 25f;
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject hitEffect;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        CastToHit();
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
                target.gotHit(baseDamage, transform.position);
            }
        }
    }

    private void CreateImpact(RaycastHit hit)
    {
       GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.2f);

    }
}
