using System.Collections.Generic;
/// <summary>
/// �÷��̾� ����
/// </summary>
public class Player_Information
{
    public Dictionary<string, int> acquiredSkills = new();

    public DefaultStat stat = null;

    public float experienceForLevelUp;
    public float experience;
    public int level;
}