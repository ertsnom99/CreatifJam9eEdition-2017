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
        //Thief = GetComponentInParent<>
        //Thief = GameObject.Find("Thief(Clone)");
        ThiefAgent = GetComponentInChildren<NavMeshAgent>();
    }

    void OnCollisionEnter (Collision collision)
    {
        //Debug.Log("I WAS TOUCHED!!!!!!!!!!!!");
        Debug.Log("I WAS TOUCHED!!!!!!!!!!!! : " + collision.gameObject.tag);
        if (collision.collider.gameObject.tag == GameManager.THROWED_BOTTLE_TAG)
        {
            
            Destroy(gameObject);
            
        }
    }

    public void StartChar() {

        //wayPointManager = GetComponent<WayPointManager>(); //WE WILL NEED THIS LATER!
        

        // TO MAKE
        //WayPointID = wayPointManager.GetValidWaypointID();

        //WayPointID = 2; //for TEST purposes
        WayPointListManager.Instance.Initialize();
        wayPointList = WayPointListManager.Instance.GetWayPointListCopy(WayPointID);



        ThiefAgent.speed = 2.0f;
        CurrentDestinationID = 0;
        CurrentDestination = getNextDestination(CurrentDestinationID);
        ThiefAgent.SetDestination(CurrentDestination);
        isInitializationComplete = true;
	}
	
    private Vector3 getNextDestination(int nextDestinationID)
    {
        //Debug.Log(nextDestinationID);
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

        if (isAgentAtDestination() == true && ++CurrentDestinationID < wayPointList.Count)
        {
            CurrentDestination = getNextDestination(CurrentDestinationID);
            ThiefAgent.SetDestination(CurrentDestination);
        }
    }
}
