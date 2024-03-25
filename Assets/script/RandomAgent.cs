using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomAgent : MonoBehaviour
{   
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.pathPending)
        {
            if(agent.remainingDistance<=agent.stoppingDistance)
            {
                if(!agent.hasPath||agent.velocity.sqrMagnitude==0f)
                {
                    NewDestination();
                }
            }
        }
    }

    private void NewDestination()
    {
        Vector3 newDest = Random.insideUnitSphere * 500f + new Vector3(139f,86f,172f);
        NavMeshHit hit;
        bool hasDestination = NavMesh.SamplePosition(newDest,out hit,100f,1);
        if(hasDestination)
        {
            agent.SetDestination(hit.position);
        }
    }
}
