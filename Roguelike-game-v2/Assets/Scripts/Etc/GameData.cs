using System.Linq;
[System.Serializable]
public class GameData
{
    private Stage_SO[] stages;
    private GameData_SO so;

    public GameData_SO SO
    {
        set
        {
            so = value;
            stages = so.stages;
        }
    }
    public Stage_SO[] Stages { get { return stages; } }
    public Stage_SO GetStageSO(string sceneName, int sign)
    {
        int index = 0;

        for(int i = 0; i < stages.Count(); i++)
        {
            if(stages[i].stageName == sceneName)
            {
                index = i + sign;

                break;
            }
        }

        if(index == stages.Count())
        {
            index = 0;
        }
        else if(index == -1)
        {
            index = stages.Count() - 1;
        }

        Managers.UserData.data.StageName = stages[index].stageName;

        return stages[index];
    }
}