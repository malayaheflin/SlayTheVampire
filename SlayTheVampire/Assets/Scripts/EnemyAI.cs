using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform playerTransform;
    public float chaseRange;
    Vector3 destination = Vector3.zero; 
    public float attackRange;
    public float wanderRadius = 10;
    public float wanderDistance = 20;
    public float wanderJitter = 1;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, 
                                                    playerTransform.position);
        if(distanceFromPlayer < chaseRange)
        {
            seek(playerTransform.position);
        }else{
            Wander();
        }
    }

    void seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector);
    }
    void Wander()
    {
        destination += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 
                                   0, 
                                   Random.Range(-1.0f, 1.0f)*wanderJitter);
        destination.Normalize();
        destination *= wanderRadius;

        Vector3 targetLocal = destination + new Vector3(0,0,wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);

        seek(targetWorld);
    }
}
