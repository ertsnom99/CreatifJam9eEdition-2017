using UnityEngine;

public class BottleThrower : MonoBehaviour
{
    [SerializeField]
    private GameObject throwOrigin;

    public GameObject ThrowOrigin
    {
        get { return throwOrigin; }
        private set { throwOrigin = value; }
    }

    public Item SelectedBottle { get; private set; }

    public float Charge { get; private set; }
    public float MaxCharge { get; private set; }

    public bool IsCharging { get; private set; }

    public float MaxThrowStrength { get; private set; }
    public float MaxTorqueStrength { get; private set; }

    [SerializeField]
    private float chargeSpeed = 0.5f;

    private void Awake ()
    {
        Charge = 0.0f;
        MaxCharge = 1.0f;
        IsCharging = false;
	    MaxThrowStrength = 20.0f;
        MaxTorqueStrength = 50.0f;

    }
	
	private void Update ()
    {
        if (IsCharging)
        {
            Charge += chargeSpeed * Time.deltaTime;
            if (Charge > MaxCharge) Charge = MaxCharge;
        }
	}

    public void SelectBottle(Item bottle)
    {
        SelectedBottle = bottle;
    }

    public void StartCharging()
    {
        IsCharging = true;
    }

    public void Throw()
    {
        // Reduce the count of the used item
        InventoryManager.GetInstance().RemoveItem(SelectedBottle.ID);

        // Place the bottle at its original position
        GameObject bottle = Instantiate(Resources.Load(SelectedBottle.PrefabName)) as GameObject;
        AlignBottle(ThrowOrigin, bottle, bottle);

        Rigidbody rigidBody = bottle.GetComponent<Rigidbody>();

        // Throw the correct bottle and make it rotate
        rigidBody.velocity = ThrowOrigin.transform.forward * Charge * MaxThrowStrength;
        rigidBody.AddTorque(bottle.transform.right * Charge * MaxTorqueStrength);

        IsCharging = false;
        Charge = 0;
    }

    private void AlignBottle(GameObject bottleHolder, GameObject bottleRef, GameObject bottle)
    {
        // Place the weapon so that the reference object will be align to and oriented exactly like the weapon holder
        bottle.transform.rotation = bottleHolder.transform.rotation * Quaternion.Inverse(bottleRef.transform.localRotation);
        bottle.transform.position -= bottleRef.transform.position - bottleHolder.transform.position;
    }

    public void StopThrowing()
    {
        IsCharging = false;
        Charge = 0;
        SelectedBottle = null;
    }
}
