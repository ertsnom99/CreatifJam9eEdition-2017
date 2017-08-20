public class Round01 : RoundInfo
{
    private int roundNo = 1;

    public override int RoundNo
    {
        get { return roundNo; }
        protected set { roundNo = value; }
    }

    private int goal = 80;

    public override int Goal
    {
        get { return goal; }
        protected set { goal = value; }
    }
}
