using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract string Name { get; protected set; }
    public abstract int Price { get; protected set; }
    public abstract int Gain { get; protected set; }
    public abstract string PrefabName { get; protected set; }
}
