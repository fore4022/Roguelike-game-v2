[System.Serializable]
public class PlayerStat
{
    public DefaultStat defaultStat;

    [PlayerStat]
    public float increaseHealth = 0;
    [PlayerStat]
    public float increaseDamage = 0;

    public PlayerStat()
    {
        defaultStat = new(10, 1, 1);
    }
}