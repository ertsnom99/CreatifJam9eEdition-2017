public class Round02 : RoundInfo
{
    private int roundNo = 2;

    public override int RoundNo
    {
        get { return roundNo; }
        protected set { roundNo = value; }
    }

    private int goal = 700;

    public override int Goal
    {
        get { return goal; }
        protected set { goal = value; }
    }
}
