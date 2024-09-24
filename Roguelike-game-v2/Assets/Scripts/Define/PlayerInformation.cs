public class PlayerInformation
{
    public float RequiredExperience { get { return basicRequiredExperience; } }
    public float Experience { get { return experience; } }
    public int Level { get { return level; } }

    public DefaultStat stat = null;//

    private float basicRequiredExperience = 0;
    private float experience = 0;
    private int level = 1;
}