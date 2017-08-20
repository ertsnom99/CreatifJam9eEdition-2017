using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject Thief;

	// Use this for initialization
	void Start () {
        GameObject SpawnLocation1 = GameObject.Find("SpawnLocation1");
        GameObject SpawnedThief = Instantiate(Thief, SpawnLocation1.transform.position, SpawnLocation1.transform.rotation);
        SpawnedThief.GetComponent<ThiefMovement>().StartChar();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
