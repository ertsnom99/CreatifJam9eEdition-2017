public class BearTrap : Item
{
    private string name = "Bear Trap";

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
