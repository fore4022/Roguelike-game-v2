public class MainScene_Manager
{
    private GameData gameData = new();

    public GameData GameData { get { return gameData; } set { gameData = value; } }
    public Stage_SO GetCurrentStage(int sign = 0)
    {
        return Managers.Main.gameData.GetStageSO(Managers.UserData.data.StageName, sign);
    }
}