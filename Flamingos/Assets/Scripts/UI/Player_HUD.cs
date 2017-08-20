using UnityEngine;
using UnityEngine.UI;

public class Player_HUD : Observer
{    
    public Text MoneyText;
    public Text GoalText;
    public Text Bottle1Text;
    public Text Bottle2Text;
    public Text Bottle3Text;
    public Text Bottle4Text;
    public Text BearTrapText;

    public void NotifyGoalChange(int goal)
    {
        GoalText.text = "Goal : " + goal;
    }

    public override void UpdateObserver()
    {
        MoneyText.text = "Money : " + InventoryManager.GetInstance().GetTotalMoney().ToString() + " $";

        float quantity = InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[0].ID);

        if (quantity == -1) quantity = Mathf.Infinity;
        
        Bottle1Text.text = ItemManager.Instance.Items[0].ItemName + " : " + quantity.ToString();
        Bottle2Text.text = ItemManager.Instance.Items[1].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[1].ID).ToString();
        Bottle3Text.text = ItemManager.Instance.Items[2].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[2].ID).ToString();
        Bottle4Text.text = ItemManager.Instance.Items[3].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[3].ID).ToString();
        BearTrapText.text = ItemManager.Instance.Items[4].ItemName + " : " + InventoryManager.GetInstance().GetAmountOfItem(ItemManager.Instance.Items[4].ID).ToString();
    }
}
