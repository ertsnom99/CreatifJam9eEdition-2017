using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class WayPointListManager : MonoSingleton<WayPointListManager>
{
    public class Vertex
    {
        public GameObject NodeGameObject;
        public int index;
    }

    public const int NR_VERTICES = 12;
    public const int NR_WAYPOINTS = 4;

    Vertex[] VertexList = new Vertex[NR_VERTICES];

    //array containing the 4 lists of vertices
    public List<Vertex>[] listOfWayPoints = new List<Vertex>[4];

    
    

    /// <summary>
    /// Initializes the adjacency matrix, ensure it's cleared and starts generating hardcoded links between the nodes
    /// </summary>
    public void Initialize()
    {
        //loads the existent Nodes from Unity and initializes the VertexList
        for (int i = 0; i < NR_VERTICES; ++i)
        {
            
            initVertex(i);
        }

        //initialize lists
        for (int i = 0; i < NR_WAYPOINTS; ++i)
        {
            listOfWayPoints[i] = new List<Vertex>();
        }
        
        //list of waypoints
        listOfWayPoints[0].Add(VertexList[1]);
        listOfWayPoints[0].Add(VertexList[2]);
        listOfWayPoints[0].Add(VertexList[3]);

        listOfWayPoints[1].Add(VertexList[0]);
        listOfWayPoints[1].Add(VertexList[1]);
        listOfWayPoints[1].Add(VertexList[4]);
        listOfWayPoints[1].Add(VertexList[5]);

        listOfWayPoints[2].Add(VertexList[0]);
        listOfWayPoints[2].Add(VertexList[6]);
        listOfWayPoints[2].Add(VertexList[7]);
        listOfWayPoints[2].Add(VertexList[8]);

        listOfWayPoints[3].Add(VertexList[9]);
        listOfWayPoints[3].Add(VertexList[10]);
        listOfWayPoints[3].Add(VertexList[11]);


    }

    private void initVertex(int inIndex)
    {
        VertexList[inIndex] = new Vertex();
        VertexList[inIndex].NodeGameObject = GameObject.Find("Node" + (inIndex) ) as GameObject;
        VertexList[inIndex].index = inIndex; 
    }

    public List<Vertex> GetWayPointListCopy(int waypointListId)
    {
        List<Vertex> waypointListCopy = new List<Vertex>();
        foreach (Vertex vertex in listOfWayPoints[waypointListId])
        {
            waypointListCopy.Add(vertex);
        }
        return waypointListCopy;
    }
}
