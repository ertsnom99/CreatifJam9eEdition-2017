using UnityEngine;

public class BottleThrower : MonoBehaviour
{
    public string SelectedBottle { get; private set; }

    public float Charge { get; private set; }
    public bool IsCharging { get; private set; }

    public float MaxThrowStrength { get; private set; }

    private float chargerSpeed = 0.5f;

    private void Awake ()
    {
        SelectedBottle = "";
        Charge = 0.0f;
	    IsCharging = false;
	    MaxThrowStrength = 1.0f;
	}
	
	private void Update ()
    {
        if (IsCharging)
        {
            Charge += chargerSpeed * Time.deltaTime;
            if (Charge > 1.0f) Charge = 1.0f;
        }
	}

    public void SelectBottle(string bottleType)
    {
        SelectedBottle = bottleType;
    }

    public void StartCharging()
    {
        IsCharging = true;
    }

    public void Throw()
    {
        // Throw the correct bottle

        SelectedBottle = "";

        IsCharging = false;
        Charge = 0;
    }
}
