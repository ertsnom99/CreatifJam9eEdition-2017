public class BottleType01 : Item
{
    private int id = 0;

    public override int ID
    {
        get { return id; }
        protected set { id = value; }
    }

    private string itemName = "Dack Janiels";

    public override string ItemName
    {
        get { return itemName; }
        protected set { itemName = value; }
    }

    private int initialAmount = -1;

    public override int InitialAmount
    {
        get { return initialAmount; }
        protected set { initialAmount = value; }
    }

    private int price;

    public override int Price
    {
        get { return price; }
        protected set { price = value; }
    }

    private int gain = 10;

    public override int Gain
    {
        get { return gain; }
        protected set { gain = value; }
    }

    private string prefabName = "BottleType01";

    public override string PrefabName
    {
        get { return prefabName; }
        protected set { prefabName = value; }
    }
}
