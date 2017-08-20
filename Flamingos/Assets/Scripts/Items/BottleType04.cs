public class BottleType04 : Item
{
    private int id = 3;

    public override int ID
    {
        get { return id; }
        protected set { id = value; }
    }

    private string itemName = "b4";

    public override string ItemName
    {
        get { return itemName; }
        protected set { itemName = value; }
    }

    private int initialAmount = 5;

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

    private int gain = 200;

    public override int Gain
    {
        get { return gain; }
        protected set { gain = value; }
    }

    private string prefabName = "BottleType04";

    public override string PrefabName
    {
        get { return prefabName; }
        protected set { prefabName = value; }
    }
}
