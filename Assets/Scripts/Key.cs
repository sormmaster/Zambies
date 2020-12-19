using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] string name = "default";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var couldPickup = FindObjectsOfType<Door>();
            foreach(Door gate in couldPickup)
            {
                gate.open(name);
            }
            Destroy(gameObject);
        }
    }
}

