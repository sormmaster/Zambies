using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammount = 5;
    [SerializeField] AmmoType type;

private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool couldPickup = FindObjectOfType<WeaponManager>().gotAmmo(type, ammount);
            if (couldPickup)
            {
                Debug.Log("Player found pick up");
                Destroy(gameObject);
            }
        }
    }
}
