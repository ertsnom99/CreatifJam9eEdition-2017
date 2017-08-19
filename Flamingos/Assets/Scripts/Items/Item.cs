using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract int ID { get; protected set; }
    public abstract string ItemName { get; protected set; }
    public abstract int InitialAmount { get; protected set; }
    public abstract int Price { get; protected set; }
    public abstract int Gain { get; protected set; }
    public abstract string PrefabName { get; protected set; }
}
