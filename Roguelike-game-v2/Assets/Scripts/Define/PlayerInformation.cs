using System.Collections.Generic;
public class PlayerInformation
{
    public Dictionary<string, int> acquiredSkills = new();

    public DefaultStat stat = null;

    public float experienceForLevelUp;
    public float experience;
    public int level;
}
/*
    public Dictionary<string, int> AcquiredSkills { get { return acquiredSkills; } }
    public DefaultStat Stat { get { return stat; } set { stat = value; } }
    public float RequiredExperience { get { return experienceForLevelUp; } }
    public float Experience { get { return experience; } set { experience = value; } }
    public int Level { get { return level; } set { level = value; } }
 */
/*
    public void SetRequiredExperience()
    {
        experienceForLevelUp += experienceForLevelUp * experienceMultiplier;
    }
 */