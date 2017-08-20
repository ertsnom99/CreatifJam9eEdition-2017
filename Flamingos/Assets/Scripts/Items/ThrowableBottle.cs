using UnityEngine;

public class ThrowableBottle : MonoBehaviour
{
    private  void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == GameManager.ROOM_TAG) Destroy(gameObject);
    }
}
