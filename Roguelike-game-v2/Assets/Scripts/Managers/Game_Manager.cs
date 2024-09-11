using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game_Manager
{
    public List<GameObject> skillList;

    public DifficultyScaler difficultyScaler;//
    public MonsterSpawner monsterSpawner;//

    public Player player;

    private StageInformation_SO stageInformation;

    private string userLevelInfoPath = "UserLevelInfoSO";

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    private void Init()
    {
        GameObject GameSystem = GameObject.Find("GameSystem");

        if (GameSystem == null)
        {
            GameSystem = new GameObject { name = "GameSystem" };

            difficultyScaler = GameSystem.AddComponent<DifficultyScaler>();
            monsterSpawner = GameSystem.AddComponent<MonsterSpawner>();
        }

        skillList = null;
    }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        Init();

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
        List<GameObject> skillList = new List<GameObject>();

        UserLevelInfo_SO userLevelInfo = await Util.LoadingToPath<UserLevelInfo_SO>(userLevelInfoPath);

        for(int i = 0; i < Managers.UserData.GetUserData.userLevel; i++)
        {
            UserLevel_SO userLevel = userLevelInfo.LevelInfo[i];

            foreach (GameObject prefab in userLevel.skillList)
            {
                skillList.Add(prefab);
            }
        }

        this.skillList = skillList;
    }
    private IEnumerator DataLoading()
    {
        Coroutine coroutine = Util.GetMonoBehaviour().StartCoroutine(Loading());

        Time.timeScale = 0;

        LoadSkillList();

        ObjectPool.CreateInstance(stageInformation.monsterList);

        yield return new WaitUntil(() => skillList != null);
        
        ObjectPool.CreateInstance(skillList);

        int typeCount = StageInformation.monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => typeCount == ObjectPool.poolingObjects.Count);

        yield return new WaitUntil(() => StageInformation.monsterList.Count == ObjectPool.scriptableObjects.Count);

        yield return new WaitUntil(() => player != null);

        Util.GetMonoBehaviour().StopCoroutine(coroutine);
        Debug.Log("Data Load Complete");

        player.Set();

        GameStart();
    }
    private IEnumerator Loading()
    {
        while(true)
        {
            Debug.Log("Data Loading");

            yield return null;
        }
    }
}