using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    float defaultZoom;
    float zoomedIn;
    [SerializeField] float zoomAdjustment = 0.75f;
    Camera zoomLens;
    FirstPersonController fpsController;

    float defaultSensitivity;
    float setSensitivity;
    [SerializeField] float Sensitivityadjustment = 1.5f;
    [SerializeField] bool isActive = true;

    bool zoomToggle = false;
    // Start is called before the first frame update
    void Start()
    {
        fpsController = GetComponentInParent<FirstPersonController>();
        zoomLens = fpsController.GetComponentInChildren<Camera>();
        defaultZoom = zoomLens.fieldOfView;
        zoomedIn = defaultZoom * zoomAdjustment;
        defaultSensitivity = fpsController.m_MouseLook.XSensitivity;
        setSensitivity = defaultSensitivity / Sensitivityadjustment;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (zoomToggle)
            {
                zoomLens.fieldOfView = defaultZoom;
                fpsController.m_MouseLook.XSensitivity = defaultSensitivity;
                fpsController.m_MouseLook.YSensitivity = defaultSensitivity;
                zoomToggle = false;
            }
            else
            {
                zoomLens.fieldOfView = zoomedIn;
                fpsController.m_MouseLook.XSensitivity = setSensitivity;
                fpsController.m_MouseLook.YSensitivity = setSensitivity;
                zoomToggle = true;
            }
            
        } 
    }
}
