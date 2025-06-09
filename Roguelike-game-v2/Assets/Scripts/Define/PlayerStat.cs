[System.Serializable]
public class PlayerStat
{
    public DefaultStat defaultStat;

    public PlayerStat()
    {
        defaultStat = new(10, 1, 1);
    }
}