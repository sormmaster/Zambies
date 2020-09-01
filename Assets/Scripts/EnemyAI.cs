using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;


    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    Boolean provoked = false;
    float provokedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(isProvoked())
        {
            EngageTarget();
            stayProvoked();
        }
        
    }

    Boolean isProvoked()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        return distanceToTarget < chaseRange || provoked;
    }

    void stayProvoked()
    {
        provoked = true;
    }

    private void EngageTarget()
    {

       if(distanceToTarget >= 1.5)
        {
            print("chasing");
            ChaseTarget();
        }

        if(distanceToTarget <= 1.5){
            print("attacking");
            AttackTarget();
        }
      
    }

    private void ChaseTarget()
    {
        stayProvoked();
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        print("attack and then continue to move" + target.name);
        print("add attack cooldown");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    
    }
}
