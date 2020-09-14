using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSC;
    [SerializeField] float range = 100f;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        Physics.Raycast(FPSC.transform.position, FPSC.transform.forward, out hit, range);
        Debug.Log("I hit this: " + hit.transform.name);
    }
}
