public class PlayerInformation
{
    public float RequiredExperience { get { return experienceForLevelUp; } }
    public float Experience { get { return experience; } set { experience = value; } }
    public int Level { get { return level; } set { level = value; } }
    public void SetRequiredExperience()
    {
        experienceForLevelUp += experienceForLevelUp * experienceMultiplier;
    }

    public DefaultStat stat = null;

    private const float baseExperience = 5;
    private const float experienceMultiplier = 0.35f;

    private float experienceForLevelUp = baseExperience;
    private float experience = 0;
    private int level = 1;
}