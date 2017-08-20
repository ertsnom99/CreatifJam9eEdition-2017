using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThiefMovement : MonoBehaviour {

    //private readonly WayPointManager wayPointManager;
    private NavMeshAgent ThiefAgent;
    public Vector3 CurrentDestination;
    public int CurrentDestinationID;
    public int WayPointID;
    private List<WayPointListManager.Vertex> wayPointList;
    private bool isInitializationComplete = false;

    // Use this for initialization
    void Awake()
    {
        GameObject Thief = GameObject.Find("Thief");
        ThiefAgent = Thief.GetComponentInChildren<NavMeshAgent>();
    }
    void Start () {

        //wayPointManager = GetComponent<WayPointManager>(); //WE WILL NEED THIS LATER!
        

        // TO MAKE
        //WayPointID = wayPointManager.GetValidWaypointID();

        //WayPointID = 2; //for TEST purposes
        WayPointListManager.Instance.Initialize();
        wayPointList = WayPointListManager.Instance.GetWayPointListCopy(WayPointID);

        ThiefAgent.speed = 1.5f;
        CurrentDestinationID = 0;
        CurrentDestination = getNextDestination(CurrentDestinationID);
        ThiefAgent.SetDestination(CurrentDestination);
        isInitializationComplete = true;
	}
	
    private Vector3 getNextDestination(int nextDestinationID)
    {
        Debug.Log(nextDestinationID);
        return wayPointList[nextDestinationID].NodeGameObject.transform.position;
    }

    private bool isAgentAtDestination()
    {
        if (isInitializationComplete == true                                     // is initialization complete?
            && !ThiefAgent.pathPending                                          // does he have a path?
            && (ThiefAgent.remainingDistance <= ThiefAgent.stoppingDistance)    // does he still have to move?
            && (!ThiefAgent.hasPath || ThiefAgent.velocity.sqrMagnitude == 0f)  // did he stop moving?
            )
        {
            return true;
        }
        return false;
    }

	// Update is called once per frame
	void Update () {
        if (isAgentAtDestination() == true && CurrentDestinationID++ < wayPointList.Count)
        {
            CurrentDestination = getNextDestination(CurrentDestinationID);
            ThiefAgent.SetDestination(CurrentDestination);
        }
    }
}
