public class BottleType02 : Item
{
    private string name = "b2";

    public override string Name
    {
        get { return name; }
        protected set { name = value; }
    }

    private int price = 20;

    public override int Price
    {
        get { return price; }
        protected set { price = value; }
    }

    private int gain = 40;

    public override int Gain
    {
        get { return gain; }
        protected set { gain = value; }
    }

    private string prefabName = "BottleType02";

    public override string PrefabName
    {
        get { return prefabName; }
        protected set { prefabName = value; }
    }
}
