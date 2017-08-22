using UnityEngine;

public class ThrowableBottle : MonoBehaviour
{
    private  void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case GameManager.ROOM_TAG:
                Destroy(gameObject);
                SoundManager.Instance.PlayOneShotSound("SFXImpact");
                break;
            case GameManager.DRUNK_TAG:
                Destroy(gameObject);
                break;
            case GameManager.CLIENT_TAG:
                InventoryManager.GetInstance().AddMoney(GetComponent<Item>().Gain);
                Destroy(gameObject);
                SoundManager.Instance.PlayOneShotSound("SFXClientServed");
                break;
        }
    }
}
