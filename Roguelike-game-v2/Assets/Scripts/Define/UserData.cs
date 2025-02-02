public class UserData//
{
    public TechnologyTree TechTree { get { return techTree; } set { techTree = value; } }
    public string StageName { get { return current_StageName; } set{ current_StageName = value; }

    private TechnologyTree techTree = new();

    private string current_StageName = "";

    private int userLevel = 1;
    private int userExperience = 0;
    private int goods = 0;
    private int lastPlayedStageIndex = 0;
}