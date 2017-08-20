public class Round03 : RoundInfo
{
    private int roundNo = 3;

    public override int RoundNo
    {
        get { return roundNo; }
        protected set { roundNo = value; }
    }

    private int minMoney = 4500;

    public override int MinMoney
    {
        get { return minMoney; }
        protected set { minMoney = value; }
    }
}
