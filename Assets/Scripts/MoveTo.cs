using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    [Tooltip("The player that the navmeshagent will be following")]
    public Transform character;

    // The navMesh on this agent
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        // The NavMeshAgent component attached to this script
        agent = GetComponent<NavMeshAgent>();

        
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the destination to be the player (this is where the NavMeshAgent will try to get to
        agent.destination = character.position;
    }
}
