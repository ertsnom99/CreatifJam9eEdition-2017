﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Dictionary<int, int> items;
    private int total_money;

    private const int INITIAL_MONEY = 1000;

    public void ResetInventory()
    {
        items = new Dictionary<int, int>();

        foreach (Item item in ItemManager.Instance.Items)
        {
            items.Add(item.ID,item.InitialAmount);
        }
        
        total_money = INITIAL_MONEY;
    }

    public void AddItem(int item_ID)
    {
        if (items[item_ID] == -1)
            { return; }
        items[item_ID]++;
    }

    public void RemoveItem(int item_ID)
    {
        if(items[item_ID] == -1)
            { return; }
        items[item_ID]--;
    }

    public void AddMoney(int money)
    {
        total_money += money;
    }

    public void RemoveMoney(int cost)
    {
        total_money -= cost;
        if (total_money < 0)
        {
            total_money = 0;
        }
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
