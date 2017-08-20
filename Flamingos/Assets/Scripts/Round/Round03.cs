public class Round03 : RoundInfo
{
    private int roundNo = 3;

    public override int RoundNo
    {
        get { return roundNo; }
        protected set { roundNo = value; }
    }

    private int goal = 4500;

    public override int Goal
    {
        get { return goal; }
        protected set { goal = value; }
    }
}
