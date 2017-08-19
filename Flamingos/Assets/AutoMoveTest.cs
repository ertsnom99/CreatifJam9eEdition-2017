using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class AutoMoveTest : MonoBehaviour {
    public NavMeshAgent agent;

    public int nextWaypointIndex;
    public int waypointID;

	// Use this for initialization
	void Start ()
    {
        GameObject Target = GameObject.Find("Destination");
        Vector3 TargetVec3 = Target.transform.position;
        agent.SetDestination(TargetVec3);

        Debug.Log(agent.hasPath);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
