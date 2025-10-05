public class MainScene_Manager
{
    private StageDatas gameData;

    public StageDatas GameData { get { return gameData; } set { gameData = value; } }
    public Stage_SO GetCurrentStageSO(int sign = 0)
    {
        return Managers.Main.gameData.GetStageSO(Managers.UserData.data.StageName, sign);
    }
}