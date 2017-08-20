using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Observable<InventoryManager>
{
    public Dictionary<int, int> items;
    private int total_money;

    private const int INITIAL_MONEY = 100;

    public void ResetInventory()
    {
        items = new Dictionary<int, int>();

        foreach (Item item in ItemManager.Instance.Items)
        {
            items.Add(item.ID,item.InitialAmount);
        }
        
        total_money = INITIAL_MONEY;
        NotifyObservers();
    }

    public void AddItem(int item_ID)
    {
        if (items[item_ID] == -1)
            { return; }
        items[item_ID]++;
        NotifyObservers();
    }

    public void RemoveItem(int item_ID)
    {
        if(items[item_ID] <= 0)
            { return; }
        items[item_ID]--;
        NotifyObservers();
    }

    public void AddMoney(int money)
    {
        total_money += money;
        NotifyObservers();
    }

    public void RemoveMoney(int cost)
    {
        total_money -= cost;
        if (total_money < 0)
        {
            total_money = 0;
        }
        NotifyObservers();
    }
    
    public int GetAmountOfItem(int item_name)
    {
        return items[item_name];
    }

    public int GetTotalMoney()
    {
        return total_money;
    }
    
}
