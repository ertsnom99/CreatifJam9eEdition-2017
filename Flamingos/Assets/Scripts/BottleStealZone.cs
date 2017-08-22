using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleStealZone : MonoBehaviour {

    public Item StolenBottleType;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.DRUNK_TAG)
        {
            InventoryManager.GetInstance().RemoveItem(StolenBottleType.ID);
            Destroy(other.gameObject);
            SoundManager.Instance.PlayOneShotSound("SFXGotRobbed");
        }
        
    }
}
