using System.Collections;
using UnityEngine;
public class GameManager
{
    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;

    public Player player;

    private StageInformation_SO stageInformation;

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
    private IEnumerator DataLoading()
    {
        Time.timeScale = 0;

        ObjectPool.CreateInstance(stageInformation.monsterList);

        yield return new WaitUntil(() => StageInformation.monsterList.Count == ObjectPool.poolingObjects.Count);//

        yield return new WaitUntil(() => StageInformation.monsterList.Count == ObjectPool.scriptableObjects.Count);//

        GameStart();
    }
}