using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField]
    private Item[] items;

    public Item[] Items
    {
        get { return items; }
        private set { items = value; }
    }

    public int GetPriceByItemID(int itemID)
    {
        int itemPrice = 0;

        foreach (Item item in Items)
        {
            if (item.ID == itemID) itemPrice = item.Price;
        }

        return itemPrice;
    }
}
