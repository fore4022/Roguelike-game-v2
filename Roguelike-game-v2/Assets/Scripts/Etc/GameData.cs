using UnityEngine;
public class GameData
{
    [SerializeField]
    private Stage_SO[] stages;

    public Stage_SO GetStageSO(string sceneName)
    {
        foreach(Stage_SO stage in stages)
        {
            if(stage.stageName == sceneName)
            {
                return stage;
            }
        }

        return null;
    }
}