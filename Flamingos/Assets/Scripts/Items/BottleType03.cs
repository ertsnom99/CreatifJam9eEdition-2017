public class BottleType03 : Item
{
    private string name = "b3";

    public override string Name
    {
        get { return name; }
        protected set { name = value; }
    }

    private int price = 50;

    public override int Price
    {
        get { return price; }
        protected set { price = value; }
    }

    private int gain = 100;

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
