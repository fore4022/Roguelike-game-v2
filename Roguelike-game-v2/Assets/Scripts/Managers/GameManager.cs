using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager
{
    public List<GameObject> skillList;

    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;

    public Player player;

    private StageInformation_SO stageInformation;

    private string userLevelInfoPath = "userLevelInfoSO";

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        GameObject GameSystem = GameObject.Find("GameSystem");

        if(GameSystem == null)
        {
            GameSystem = new GameObject { name = "GameSystem" };

            difficultyScaler = GameSystem.AddComponent<DifficultyScaler>();
            monsterSpawner = GameSystem.AddComponent<MonsterSpawner>();
        }

        this.stageInformation = stageInformation;

        Util.GetMonoBehaviour().StartCoroutine(DataLoading());
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
    public void GameStart()
    {
        Time.timeScale = 1;
    }
    public void GameEnd()
    {
        Time.timeScale = 0;

        //
    }
    private async void LoadSkillList()
    {

    }
    private IEnumerator DataLoading()
    {
        Time.timeScale = 0;

        ObjectPool.CreateInstance(stageInformation.monsterList);

        LoadSkillList();

        yield return new WaitUntil(() => skillList != null);

        int typeCount = StageInformation.monsterList.Count;

        yield return new WaitUntil(() => typeCount == ObjectPool.poolingObjects.Count);

        yield return new WaitUntil(() => StageInformation.monsterList.Count == ObjectPool.scriptableObjects.Count);

        GameStart();
    }
}
/*
        foreach(UserLevel_SO userLevel in await Util.LoadToPath<UserLevelInfo_SO>(userLevelInfoPath).Result)
        {
            
        } 
 */