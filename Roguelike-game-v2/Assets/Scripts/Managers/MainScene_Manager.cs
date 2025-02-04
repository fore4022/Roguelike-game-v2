public class MainScene_Manager
{
    private GameData gameData = null;

    public GameData GameData
    {
        get { return gameData; }
        set
        {
            Init();
            gameData = value;
        }
    }
    public Stage_SO GetCurrentStage()
    {
        return Managers.Main.gameData.GetStageSO(Managers.UserData.data.StageName);
    }
    private void Init()
    {
        var stageClearInfo = Managers.UserData.data.StageClearInfo;
        string stageName;

        foreach (Stage_SO stage in gameData.Stages)
        {
            stageName = stage.stageName;

            if(!stageClearInfo.ContainsKey(stageName))
            {
                Managers.UserData.data.StageClearInfo.Add(stageName, false);
            }
        }
    }
}