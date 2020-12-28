using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int hitPoints = 100;

    [SerializeField] ParticleSystem hitVfx;

    [SerializeField] bool isBoss = false;

    private bool isDead = false;

    public bool gotHit(int damage, Vector3 location)
    {
        if (isDead)
        {
            return false;
        }
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        
        playHitVFX(location);
        if (hitPoints <= 0)
        {
            endME();
            isDead = true;
            FindObjectOfType<AudioController>().Play("enemyDie");
            float toHeal = Mathf.Clamp((0 - hitPoints), 10, 25);
            FindObjectOfType<PlayerHealth>().heal(toHeal);
            if (isBoss)
            {
                FindObjectOfType<SceneLoader>().theyWon();
            }
            return true;
        } else
        {
            FindObjectOfType<AudioController>().Play("enemyHit");
            return false;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void playHitVFX(Vector3 location)
    {
        Vector3 direction = (location - hitVfx.transform.position).normalized;
        hitVfx.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
         hitVfx.Play();
    }

    private void endME()
    {
        GetComponent<Animator>().Play("Death");
        Destroy(gameObject, 15.0f);
    }
}
