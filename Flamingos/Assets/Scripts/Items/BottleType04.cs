public class BottleType04 : Item
{
    private string name = "b4";

    public override string Name
    {
        get { return name; }
        protected set { name = value; }
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
