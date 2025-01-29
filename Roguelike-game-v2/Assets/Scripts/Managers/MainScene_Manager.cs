public class MainScene_Manager
{
    public GameData gameData = null;

    private Stage_SO currentStage;

    public Stage_SO GetCurrentStage { get { return currentStage; } set { currentStage = value; } }
}