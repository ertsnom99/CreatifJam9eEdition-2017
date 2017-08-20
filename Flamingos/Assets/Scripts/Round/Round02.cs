public class Round02 : RoundInfo
{
    private int roundNo = 2;

    public override int RoundNo
    {
        get { return roundNo; }
        protected set { roundNo = value; }
    }

    private int minMoney = 1500;

    public override int MinMoney
    {
        get { return minMoney; }
        protected set { minMoney = value; }
    }
}
