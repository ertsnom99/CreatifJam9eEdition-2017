﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class ChargeBar : MonoBehaviour
{
    [SerializeField]
    private BottleThrower bottleThrowerScript;
    
    private Slider chargeBar;

    private void Awake()
    {
        chargeBar = GetComponent<Slider>();
    }

    private void Start()
    {
        chargeBar.maxValue = bottleThrowerScript.MaxCharge;
    }

    private void Update ()
    {
        chargeBar.value = bottleThrowerScript.Charge;
	}
}
