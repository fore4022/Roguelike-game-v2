[System.Serializable]
public class PlayerStat
{
    public DefaultStat defaultStat;

    public float healthRegenPerSec = 0;
    public float increaseHealth = 0;
    public float increaseDamage = 0;

    public PlayerStat()
    {
        defaultStat = new(10, 1, 1);
    }
}