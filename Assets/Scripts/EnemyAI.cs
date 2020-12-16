using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float detectRange = 15f;
    [SerializeField] float chaseRange = 30f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float baseSpeed = 3.5f;
    [SerializeField] float anger = 10.0f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private float lastProvoked;
    private float aggresion = 0;
    Boolean provoked = false;
    EnemyHealth health;

    
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (provoked || canDetect() || InRange())
        {
            EngageTarget();
        } 
        
    }

    private void EngageTarget()
    {
        if (distanceToTarget <= 2.0)
        {
            AttackTarget();
        }else if (distanceToTarget > 2.0 && (provoked || InRange()))
        {
            animator.SetBool("attack", false);
            ChaseTarget();
        } else
        {
            animator.SetTrigger("idle");
        }
    }
    private Boolean InRange()
    {
        return distanceToTarget <= chaseRange;
    }

    private Boolean canDetect()
    {
        return distanceToTarget < detectRange;
    }

    private void SetSpeed()
    {
        if (provoked)
        {
            navMeshAgent.speed = baseSpeed * (1.5f * aggresion);
        } else if (canDetect())
        {
            navMeshAgent.speed = baseSpeed * 1.0f;
        } else
        {
            navMeshAgent.speed = baseSpeed * 0.75f;
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.isStopped = false;
        animator.SetTrigger("move");
        SetSpeed();
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        navMeshAgent.isStopped = true;
        Vector3 direction = (target.position - transform.position).normalized;
        if (Vector3.Dot(transform.forward, direction) > 0.7f)
        {
            animator.SetBool("attack", true);
        } else
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }
        
    }

    public void OnDamageTaken()
    {
        aggresion++;
        StartCoroutine(GotProvoked());
        SetSpeed();
        
    }

    private IEnumerator GotProvoked()
    {
        lastProvoked = Time.time;
       
        while (Time.time - lastProvoked <= anger || InRange())
        {
            provoked = true;
            yield return new WaitForSeconds(1f);
        }
        provoked = false;
        aggresion = 0;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    
    }
}
