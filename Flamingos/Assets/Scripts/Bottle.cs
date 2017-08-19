using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField]
    private string bottleType;

    public string BottleType
    {
        get { return bottleType; }
        private set { bottleType = value; }
    }

}
