using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataInit
{
    public UserLevelInfo_SO userLevelInfo;
    public ObjectPool objectPool = null;

    private const string userLevelInfoPath = "UserLevelInfoSO";
    private const string inGameScene = "InGame";
    private const int defaultMonsterCount = 750;
    private const int defaultSkillCount = 250;

    public void GetInGameData()
    {
        Util.GetMonoBehaviour().StartCoroutine(Init());
    }
    public void GetMonsterList(ref List<GameObject> monsterList)
    {
        List<GameObject> list = new();

        foreach (SpawnInformation_SO so in Managers.Game.stageInformation.spawnInformationList)
        {
            foreach (SpawnInformation info in so.monsterInformation)
            {
                list.Add(info.monster);
            }
        }

        monsterList = list;
    }
    public void LoadSkillList(ref List<GameObject> skillList)
    {
        List<GameObject> list = new();

        for(int i = 0; i < Managers.UserData.GetUserData.userLevel; i++)
        {
            UserLevel_SO userLevel = userLevelInfo.LevelInfo[i];

            foreach(AttackInformation_SO so in userLevel.attackInformationList)
            {
                list.Add(so.skillObject);

                Managers.Game.inGameData.attackData.SetDictionaryItem(so);
            }
        }

        skillList = list;
    }
    private IEnumerator Init()
    {
        Time.timeScale = 0;

        Managers.Scene.LoadScene(inGameScene);

        yield return new WaitUntil(() => inGameScene == Managers.Scene.CurrentScene);

        GameObject GameSystem = GameObject.Find("GameSystem");

        objectPool = new();

        if (GameSystem == null)
        {
            GameSystem = new GameObject { name = "GameSystem" };

            Managers.Game.monsterSpawner = GameSystem.AddComponent<MonsterSpawner>();
            Managers.Game.inGameTimer = GameSystem.AddComponent<InGameTimer>();
        }

        Util.GetMonoBehaviour().StartCoroutine(DataLoading());
    }
    private async void LoadUserLevelInfo()
    {
        userLevelInfo = await Util.LoadingToPath<UserLevelInfo_SO>(userLevelInfoPath);
    }
    private IEnumerator DataLoading()
    {
        List<GameObject> monsterList = new();
        List<GameObject> skillList = new();

        Time.timeScale = 0;
        
        LoadUserLevelInfo();

        yield return new WaitUntil(() => userLevelInfo != null);

        GetMonsterList(ref monsterList);
        LoadSkillList(ref skillList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        Managers.Game.monsterSpawner.monsterList = monsterList;

        Managers.Game.inGameData.dataInit.objectPool.CreateObjects(monsterList, defaultMonsterCount);
        Managers.Game.inGameData.dataInit.objectPool.CreateObjects(skillList, defaultSkillCount);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => typeCount == Managers.Game.inGameData.dataInit.objectPool.PoolingObjectsCount);

        yield return new WaitUntil(() => typeCount == Managers.Game.inGameData.dataInit.objectPool.ScriptableObjectsCount);
        
        yield return new WaitUntil(() => Managers.Game.inGameData.playerData.levelUpdate != null);

        Time.timeScale = 1;

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() == null);

        Managers.Game.GameStart();
    }
}