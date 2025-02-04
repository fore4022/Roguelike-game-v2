using System.Collections.Generic;
public class UserData//
{
    private Dictionary<string, bool> stageClearInfo = new();
    private TechnologyTree techTree = new();

    private string current_StageName = "prairie";
    private int userLevel = 1;
    private int userExperience = 0;

    public Dictionary<string, bool> StageClearInfo { get { return stageClearInfo; } set { stageClearInfo = value; } }
    public TechnologyTree TechTree { get { return techTree; } set { techTree = value; } }
    public string StageName { get { return current_StageName; } set { current_StageName = value; } }
    public int UserLevel { get { return userLevel; } set { userLevel = value; } }
    public int UserExperience { get { return userExperience; } set { userExperience = value; } }
    public bool isClear()
    {
        return stageClearInfo[current_StageName];
    }
}