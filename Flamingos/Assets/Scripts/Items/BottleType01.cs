public class BottleType01 : Item
{
    private string name = "b1";

    public override string Name
    {
        get { return name; }
        protected set { name = value; }
    }

    private int price;

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

    private string prefabName = "BottleType01";

    public override string PrefabName
    {
        get { return prefabName; }
        protected set { prefabName = value; }
    }
}
