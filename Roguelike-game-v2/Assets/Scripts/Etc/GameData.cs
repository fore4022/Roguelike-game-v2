using System.Linq;
using UnityEngine;
public class GameData
{
    [SerializeField]
    private Stage_SO[] stages;//

    public Stage_SO GetStageSO(string sceneName, int sign = 0)
    {
        foreach(Stage_SO stage in stages)//
        {
            if(stage.stageName == sceneName)
            {
                return stage;
            }
        }

        for(int i = 0; i < stages.Count(); i++)
        {
            if(stages[i].stageName == sceneName)
            {

            }
        }

        return null;
    }
}