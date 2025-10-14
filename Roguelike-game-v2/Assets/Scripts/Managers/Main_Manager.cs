public class Main_Manager
{
    public StageDatas stageDatas = new();

    public Stage_SO GetCurrentStageSO(int sign = 0)
    {
        return Managers.Main.stageDatas.GetSO(Managers.Data.data.StageName, sign);
    }
}