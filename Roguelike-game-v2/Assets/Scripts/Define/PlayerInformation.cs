using System.Collections.Generic;
public class PlayerInformation
{
    public Dictionary<string, int> acquiredSkills = new();

    public DefaultStat stat = null;

    public const float baseExperience = 5;
    public const float experienceMultiplier = 0.35f;

    public float experienceForLevelUp = baseExperience;
    public float experience = 0;
    public int level = 1;
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