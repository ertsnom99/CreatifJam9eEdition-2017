public class BearTrap : Item
{
    private int id = 4;

    public override int ID
    {
        get { return id; }
        protected set { id = value; }
    }

    private string itemName = "Bear Trap";

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

    private int price = 100;

    public override int Price
    {
        get { return price; }
        protected set { price = value; }
    }

    private int gain;

    public override int Gain
    {
        get { return gain; }
        protected set { gain = value; }
    }

    private string prefabName = "BearTrap";

    public override string PrefabName
    {
        get { return prefabName; }
        protected set { prefabName = value; }
    }
}
