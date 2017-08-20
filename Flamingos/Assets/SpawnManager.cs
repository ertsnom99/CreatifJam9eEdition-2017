using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager> {
    public bool isEnabled = false;
    public const float drunkSpawnFrequency = 3.0f;
    public GameObject Drunk;

    private float onPauseRemainDelayTillNextDrunkSpawn = 2.0f;
    private ArrayList spawnedDrunks = new ArrayList();
    private Stopwatch timer = new Stopwatch();
    private GameObject[] SpawnDrunkLocationArray;
    

    // Use this for initialization
    void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        SpawnDrunkLocationArray = GameObject.FindGameObjectsWithTag("SpawnDrunkLocation");
        UnityEngine.Debug.Log("");
        // VERIFY IF SpawnDrunkLocationArray CONTAINS THE 3 DRUNK SPAWN LOCATIONS
    }

    public void StartSpawningDrunks()
    {
        InvokeRepeating("SpawnDrunk", onPauseRemainDelayTillNextDrunkSpawn, drunkSpawnFrequency);
        timer.Start();
    }

    public void StopSpawningDrunks()
    {
        CancelInvoke("SpawnDrunk");
        timer.Stop();
        onPauseRemainDelayTillNextDrunkSpawn = (float)((timer.ElapsedMilliseconds / 1000)%3);
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

    public void KillAllDrunks()
    {
        foreach (GameObject SpawnedDrunk in spawnedDrunks)
        {
            Destroy(SpawnedDrunk);
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
