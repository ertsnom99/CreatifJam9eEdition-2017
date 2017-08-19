using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private int total_money;
    private int total_bottles;
    private int total_empty_bottles;

    public void BeginNewGame()
    {
        total_money = 100;
        total_bottles = 10;
    }

    public void AddMoney(int money)
    {
        total_money += money;
    }

    public void RemoveMoney(int cost)
    {
        total_money -= cost;
    }

    public void AddBottles(int amount_bottles)
    {
        total_bottles += amount_bottles;
    }

    public void RemoveBottles(int lost_bottles)
    {
        total_bottles += lost_bottles;
    }
}
