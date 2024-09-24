public class PlayerInformation
{
    public float RequiredExperience { get { return experienceForLevelUp; } }

    public DefaultStat stat = null;

    private const float baseExperience = 5;

    private float experienceForLevelUp;
    private float experience = 0;
    private int level = 1;
}