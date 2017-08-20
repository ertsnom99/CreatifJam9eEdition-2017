public class Round01 : RoundInfo
{
    private int roundNo = 1;

    public override int RoundNo
    {
        get { return roundNo; }
        protected set { roundNo = value; }
    }

    private int minMoney = 500;

    public override int MinMoney
    {
        get { return minMoney; }
        protected set { minMoney = value; }
    }
}
