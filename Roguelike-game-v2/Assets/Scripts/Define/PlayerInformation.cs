using System.Collections.Generic;
public class PlayerInformation
{
    public Dictionary<string, int> acquiredSkills = new();

    public DefaultStat stat = null;

    public float experienceForLevelUp;
    public float experience;
    public int level;
}