using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 100;

    [SerializeField] ParticleSystem hitVfx;

    private bool isDying = false;

    public bool gotHit(int damage, Vector3 location)
    {
        if (isDying)
        {
            return false;
        }
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        
        playHitVFX(location);
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            isDying = true;
            return true;
        } else
        {
            return false;
        }
    }

    private void playHitVFX(Vector3 location)
    {
        Vector3 direction = (location - hitVfx.transform.position).normalized;
        hitVfx.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
         hitVfx.Play();
    }

}
