using System.Linq;
using UnityEngine;
public class GameData
{
    [SerializeField]
    private Stage_SO[] stages;

    public Stage_SO GetStageSO(string sceneName, int sign = 0)
    {
        int index = 0;

        for(int i = 0; i < stages.Count(); i++)
        {
            if(stages[i].stageName == sceneName)
            {
                index = i + sign;
            }
        }

        if(index > stages.Count())
        {
            index = 0;
        }
        else if(index == -1)
        {
            index = stages.Count();
        }

        return stages[index];
    }
}