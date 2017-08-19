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

    public int GetPriceByItemName(string itemName)
    {
        int itemPrice = 0;

        foreach (Item item in Items)
        {
            if (item.Name == itemName) itemPrice = item.Price;
        }

        return itemPrice;
    }
}
