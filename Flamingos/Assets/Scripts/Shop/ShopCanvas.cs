using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour {

    private const int BOTTLE_1 = 0;
    private const int BOTTLE_2 = 1;
    private const int BOTTLE_3 = 2;
    private const int BOTTLE_4 = 3;
    private const int BEAR_TRAP = 4;

    internal void SetCallBackMethodOnClose()
    {
        throw new NotImplementedException();
    }

    public Text MoneyText;
    public Text Bottle2PriceText;
    public Text Bottle3PriceText;
    public Text Bottle4PriceText;
    public Text BearTrapPriceText;
    public Text NotEnoughMoneyText;

    public Button BuyBottle2Button;
    public Button BuyBottle3Button;
    public Button BuyBottle4Button;
    public Button BuyBearTrapButton;

    private Action callBackMethod;

    void Start()
    {
        NotEnoughMoneyText.text = "";
        MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";
        Bottle2PriceText.text = ItemManager.Instance.GetPriceByItemID(BOTTLE_2).ToString() + " $";
        Bottle3PriceText.text = ItemManager.Instance.GetPriceByItemID(BOTTLE_3).ToString() + " $";
        Bottle4PriceText.text = ItemManager.Instance.GetPriceByItemID(BOTTLE_4).ToString() + " $";
        BearTrapPriceText.text = ItemManager.Instance.GetPriceByItemID(BEAR_TRAP).ToString() + " $";
    }

    public void SetCallBackMethodOnClose(Action method)
    {
        callBackMethod = method;
    }

    public void ExitShop()
    {
        Destroy(gameObject);
        if (callBackMethod != null) callBackMethod();
    }
    
    public void BuyItem(int item_ID)
    {
        NotEnoughMoneyText.text = "";
        switch (item_ID)
        {
            case BOTTLE_2:
                if (InventoryManager.GetInstance().GetTotalMoney() >= ItemManager.Instance.GetPriceByItemID(BOTTLE_2))
                {
                    InventoryManager.GetInstance().AddItem(BOTTLE_2);
                    InventoryManager.GetInstance().RemoveMoney(ItemManager.Instance.GetPriceByItemID(BOTTLE_2));
                    MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";
                }
                else
                    { NotEnoughMoneyText.text = "Not Enough Money!"; }
                break;
            case BOTTLE_3:
                if (InventoryManager.GetInstance().GetTotalMoney() >= ItemManager.Instance.GetPriceByItemID(BOTTLE_3))
                {
                    InventoryManager.GetInstance().AddItem(BOTTLE_3);
                    InventoryManager.GetInstance().RemoveMoney(ItemManager.Instance.GetPriceByItemID(BOTTLE_3));
                    MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";
                }
                else
                    { NotEnoughMoneyText.text = "Not Enough Money!"; }
                break;
            case BOTTLE_4:
                if (InventoryManager.GetInstance().GetTotalMoney() >= ItemManager.Instance.GetPriceByItemID(BOTTLE_4))
                {
                    InventoryManager.GetInstance().AddItem(BOTTLE_4);
                    InventoryManager.GetInstance().RemoveMoney(ItemManager.Instance.GetPriceByItemID(BOTTLE_4));
                    MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";
                }
                else
                    { NotEnoughMoneyText.text = "Not Enough Money!"; }
                break;
            case BEAR_TRAP:
                if (InventoryManager.GetInstance().GetTotalMoney() >= ItemManager.Instance.GetPriceByItemID(BEAR_TRAP))
                {
                    InventoryManager.GetInstance().AddItem(BEAR_TRAP);
                    InventoryManager.GetInstance().RemoveMoney(ItemManager.Instance.GetPriceByItemID(BEAR_TRAP));
                    MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";
                }
                else
                    { NotEnoughMoneyText.text = "Not Enough Money!"; }
                break;
        }
            
    }
}
