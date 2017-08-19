using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HUD : MonoBehaviour {
    
    public Text MoneyText;
    public Text Bottle1Text;
    public Text Bottle2Text;
    public Text Bottle3Text;
    public Text Bottle4Text;
    public Text BearTrapText;

    void Update()
    {
        
        MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";
        Bottle1Text.text = ItemManager.Instance.Items[0].ItemName + " : " +InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[0].ID).ToString();
        Bottle2Text.text = ItemManager.Instance.Items[1].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[1].ID).ToString();
        Bottle3Text.text = ItemManager.Instance.Items[2].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[2].ID).ToString();
        Bottle4Text.text = ItemManager.Instance.Items[3].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[3].ID).ToString();
        BearTrapText.text = ItemManager.Instance.Items[4].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[4].ID).ToString();
    }
    
    
}
