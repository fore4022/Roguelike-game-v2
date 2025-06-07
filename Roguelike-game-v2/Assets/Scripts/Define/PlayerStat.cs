[System.Serializable]
public class PlayerStat
{
    public DefaultStat defaultStat;

    [ShowInInspector]
    public float increaseHealth = 0;
    [ShowInInspector]
    public float increaseDamage = 0;

    public PlayerStat()
    {
        defaultStat = new(10, 1, 1);
    }
}