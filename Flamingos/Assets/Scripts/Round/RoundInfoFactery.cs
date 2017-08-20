public class RoundInfoFactery : Singleton<RoundInfoFactery>
{
	public RoundInfo GetRoundInfo (int round)
    {
        switch (round)
        {
            case 1:
                return new Round01();
                break;
            case 2:
                return new Round02();
                break;
            case 3:
                return new Round03();
                break;
            default:
                return null;
                break;
        }
    }
}
