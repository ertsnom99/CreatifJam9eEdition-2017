using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager> {
    public bool isEnabled = false;
    public const float drunkSpawnFrequency = 3.0f;
    public float clientSpawnFrequency = 5.0f;
    public GameObject Drunk;

    public GameObject Client;
    

    private float onPauseRemainDelayTillNextDrunkSpawn = 2.0f;
    private float onPauseRemainDelayTillNextClientSpawn = 0.0f;
    private ArrayList spawnedDrunks = new ArrayList();
    private ArrayList spawnedClients = new ArrayList();
    private Stopwatch timer = new Stopwatch();

    private GameObject[] SpawnDrunkLocationArray;
    private GameObject[] SpawnClientLocationArray;
    private bool[] OccupiedClientLocationArray;


    // Use this for initialization
    void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        SpawnDrunkLocationArray = GameObject.FindGameObjectsWithTag("SpawnDrunkLocation");
        SpawnClientLocationArray = GameObject.FindGameObjectsWithTag("SpawnClientLocation");
        OccupiedClientLocationArray = new bool[SpawnClientLocationArray.Length];

        for (int i = 0; i < OccupiedClientLocationArray.Length; ++i) 
        {
            OccupiedClientLocationArray[i] = false;
        }
        
        // VERIFY IF SpawnDrunkLocationArray CONTAINS THE 3 DRUNK SPAWN LOCATIONS
    }

    public void StartSpawningClientsAndDrunks()
    {
        InvokeRepeating("SpawnDrunk", onPauseRemainDelayTillNextDrunkSpawn, drunkSpawnFrequency);
        InvokeRepeating("SpawnClient", onPauseRemainDelayTillNextClientSpawn, clientSpawnFrequency);
        timer.Start();
    }

    public void StopSpawningClientsAndDrunks()
    {
        CancelInvoke("SpawnDrunk");
        CancelInvoke("SpawnClient");
        timer.Stop();
        onPauseRemainDelayTillNextDrunkSpawn = (float)((timer.ElapsedMilliseconds / 1000 ) % (int)drunkSpawnFrequency);
        onPauseRemainDelayTillNextClientSpawn = (float)((timer.ElapsedMilliseconds / 1000) % (int)clientSpawnFrequency);
    }

    private int getActualSpawnID(string spawnLocationName)
    {
        string lastLetterOfSpawnLocationName = ""; lastLetterOfSpawnLocationName += spawnLocationName[spawnLocationName.Length - 1];
        int actualSpawnID = int.Parse(lastLetterOfSpawnLocationName);
        return actualSpawnID;
    }
    
    private void SpawnDrunk()
    {
        int SpawnLocationID = GetDrunkSpawnLocation();

        GameObject spawnLocation = SpawnDrunkLocationArray[SpawnLocationID];
        GameObject newlySpawned = Instantiate(Drunk, spawnLocation.transform.position, spawnLocation.transform.rotation);
        ;


        switch (getActualSpawnID(spawnLocation.name))
        {
            case 1:
            {
                    newlySpawned.GetComponent<ThiefMovement>().WayPointID = Random.Range(1, 3);
                    break;
            }
            case 2:
            {
                    newlySpawned.GetComponent<ThiefMovement>().WayPointID = 0;
                    break;
            }
            case 3:
            {
                    newlySpawned.GetComponent<ThiefMovement>().WayPointID = 3;
                    break;
            }

        };

        newlySpawned.GetComponent<ThiefMovement>().StartChar();

        spawnedDrunks.Add(newlySpawned);    
    }

    private int countNumAvailableClientLocations()
    {
        int countNumAvailableClientLocations = 0;
        for (int i = 0; i < OccupiedClientLocationArray.Length; ++i)
        {
            if (OccupiedClientLocationArray[i] == false)
            {
                countNumAvailableClientLocations++;
            }
        }
        return countNumAvailableClientLocations;
    }

    private int findActualPosInCusSpwnLocArray(int randomGenNr)
    {
        int count = 0;
        for (int i = 0; i < OccupiedClientLocationArray.Length; ++i)
        {
            if (OccupiedClientLocationArray[i] == false)
            {
                if (randomGenNr == count)
                {
                    return i;
                }
                count++;
            }
        }
        return -1;
    }

    public void SpawnClient()
    {
        int numAvailableClientLocations = countNumAvailableClientLocations();
        if (numAvailableClientLocations == 0)
        {
            return;
        }

        int chosenLocation = Random.Range(0, numAvailableClientLocations);
        int locationInArray = findActualPosInCusSpwnLocArray(chosenLocation);

        GameObject SpawnedClientLocation = SpawnClientLocationArray[locationInArray];

        GameObject newlySpawned = Instantiate(Client, SpawnedClientLocation.transform.position, SpawnedClientLocation.transform.rotation);

        OccupiedClientLocationArray[locationInArray] = true;
        spawnedClients.Add(newlySpawned);       
    }

    public void KillAllDrunksAndClients()
    {
        foreach (GameObject SpawnedDrunk in spawnedDrunks)
        {
            Destroy(SpawnedDrunk);
        }

        foreach (GameObject SpawnedClient in spawnedClients)
        {
            Destroy(SpawnedClient);
        }

    }

    private int GetDrunkSpawnLocation()
    {
        int SpawnLocationID = Random.Range(0, 3); //return 0,1,2
        return SpawnLocationID;
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }




	// Update is called once per frame
	void Update () {
		
	}
}
