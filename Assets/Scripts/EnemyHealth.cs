using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    [SerializeField] ParticleSystem hitVfx;

    public void gotHit(float damage, Vector3 location)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            Destroy(gameObject);
        }
        playHitVFX(location);
    }

    private void playHitVFX(Vector3 location)
    {
        Vector3 direction = (location - hitVfx.transform.position).normalized;
        hitVfx.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
         hitVfx.Play();
    }

}
