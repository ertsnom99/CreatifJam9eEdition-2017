﻿public class BottleType03 : Item
{
    private int id = 2;

    public override int ID
    {
        get { return id; }
        protected set { id = value; }
    }

    private string itemName = "Johnnie Water";

    public override string ItemName
    {
        get { return itemName; }
        protected set { itemName = value; }
    }

    private int initialAmount;

    public override int InitialAmount
    {
        get { return initialAmount; }
        protected set { initialAmount = value; }
    }

    private int price = 30;

    public override int Price
    {
        get { return price; }
        protected set { price = value; }
    }

    private int gain = 75;

    public override int Gain
    {
        get { return gain; }
        protected set { gain = value; }
    }

    private string prefabName = "BottleType03";

    public override string PrefabName
    {
        get { return prefabName; }
        protected set { prefabName = value; }
    }
}
