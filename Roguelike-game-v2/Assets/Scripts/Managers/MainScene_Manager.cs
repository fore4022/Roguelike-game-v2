using System.Collections;
using UnityEngine;
public class MainScene_Manager
{
    private GameData gameData = null;

    public GameData GameData
    {
        get { return gameData; }
        set
        {
            gameData = value;

            Util.GetMonoBehaviour().StartCoroutine(Init());
        }
    }
    public Stage_SO GetCurrentStage(int sign = 0)
    {
        return Managers.Main.gameData.GetStageSO(Managers.UserData.data.StageName, sign);
    }
    private IEnumerator Init()
    {
        string stageName;

        yield return new WaitUntil(() => Managers.UserData.data != null);

        foreach (Stage_SO stage in gameData.Stages)
        {
            stageName = stage.stageName;

            if(!Managers.UserData.data.StageClearInfo.ContainsKey(stageName))
            {
                Managers.UserData.data.StageClearInfo.Add(stageName, false);
            }
        }
    }
}