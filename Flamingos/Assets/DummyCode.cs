﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCode : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	    InventoryManager.GetInstance().ResetInventory();   
        Debug.LogWarning("Resetted!");	
	}
}
