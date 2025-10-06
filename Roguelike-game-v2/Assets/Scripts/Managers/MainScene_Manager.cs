public class MainScene_Manager
{
    private StageDatas stageDatas;

    public StageDatas StageDatas { get { return stageDatas; } set { stageDatas = value; } }
    public Stage_SO GetCurrentStageSO(int sign = 0)
    {
        return Managers.Main.stageDatas.GetSO(Managers.UserData.data.StageName, sign);
    }
}