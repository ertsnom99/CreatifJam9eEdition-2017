using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ClientBehavior : MonoBehaviour {

    public float timeUntilDisappearance;

    
    // Use this for initialization
    void Start () {
        timeUntilDisappearance = 3.5f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == GameManager.THROWED_BOTTLE_TAG)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        timeUntilDisappearance -= Time.deltaTime;
        if (timeUntilDisappearance <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
