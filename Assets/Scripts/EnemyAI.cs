using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 15f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    Boolean provoked = false;
    float provokedTime = 0f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(isProvoked() || provoked)
        {
            EngageTarget();
        } else
        {
            animator.SetTrigger("idle");
        }
        
    }

    Boolean isProvoked()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        return distanceToTarget < chaseRange;

    }


    private void EngageTarget()
    {
        if (distanceToTarget <= 2.0)
        {

            AttackTarget();
            provoked = false;
        }else if (distanceToTarget > 2.0 && distanceToTarget <= chaseRange * 2)
        {
            animator.SetBool("attack", false);
            ChaseTarget();
        }else
        {
            provoked = false;
            animator.SetBool("attack", false);
        }
      
    }

    private void ChaseTarget()
    {
        navMeshAgent.isStopped = false;
        animator.SetTrigger("move");
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
        provoked = true;
        navMeshAgent.speed = navMeshAgent.speed * 1.5f;
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    
    }
}
