using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Game_Manager
{
    public List<GameObject> skillList;

    public DifficultyScaler difficultyScaler;
    public MonsterSpawner monsterSpawner;
    public InGameTimer inGameTimer;
    public Player player;

    private StageInformation_SO stageInformation;

    private string userLevelInfoPath = "UserLevelInfoSO";
    private string inGameScene = "InGame";

    public StageInformation_SO StageInformation { get { return stageInformation; } }
    public void DataLoad(StageInformation_SO stageInformation)
    {
        Util.GetMonoBehaviour().StartCoroutine(Init());

        this.stageInformation = stageInformation;
    }
    private void Set()
    {
        difficultyScaler = new();

        monsterSpawner.Set();
        inGameTimer.Set();
        player.Set();
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
    public void GetMonsterList(ref List<GameObject> monsterList)
    {
        List<GameObject> list = new();

        foreach (SpawnInformation_SO so in stageInformation.spawnInformationList)
        {
            foreach (SpawnInformation info in so.spawnInformation)
            {
                list.Add(info.monster);
            }
        }

        monsterList = list;
    }
    private IEnumerator Init()
    {
        Managers.Scene.LoadScene(inGameScene);

        yield return new WaitUntil(() => inGameScene == Managers.Scene.scenePath);

        GameObject GameSystem = GameObject.Find("GameSystem");

        if (GameSystem == null)
        {
            GameSystem = new GameObject { name = "GameSystem" };

            monsterSpawner = GameSystem.AddComponent<MonsterSpawner>();
            inGameTimer = GameSystem.AddComponent<InGameTimer>();
        }

        skillList = null;

        Util.GetMonoBehaviour().StartCoroutine(DataLoading());
    }
    private IEnumerator DataLoading()
    {
        List<GameObject> monsterList = new();

        Coroutine coroutine = Util.GetMonoBehaviour().StartCoroutine(Loading());

        Time.timeScale = 0;

        LoadSkillList();
        GetMonsterList(ref monsterList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        ObjectPool.CreateInstance(skillList);
        ObjectPool.CreateInstance(monsterList);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => typeCount == ObjectPool.poolingObjects.Count);

        yield return new WaitUntil(() => typeCount == ObjectPool.scriptableObjects.Count);

        yield return new WaitUntil(() => player != null);

        Util.GetMonoBehaviour().StopCoroutine(coroutine);
        Debug.Log("Data Load Complete");//

        Set();

        GameStart();
    }
    private IEnumerator Loading()//
    {
        while(true)
        {
            Debug.Log("Data Loading");

            yield return null;
        }
    }
}