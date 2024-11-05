using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameDataInit
{
    public UserLevelInfo_SO userLevelInfo;

    public string userLevelInfoPath = "UserLevelInfoSO";
    public string inGameScene = "InGame";

    private const int defaultMonsterCount = 75;
    private const int defaultSkillCount = 25;

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

                Managers.Game.attackData.SetDictionaryItem(so);
            }
        }

        skillList = list;
    }
    public IEnumerator Init()
    {
        Managers.Scene.LoadScene(inGameScene);

        yield return new WaitUntil(() => inGameScene == Managers.Scene.scenePath);

        GameObject GameSystem = GameObject.Find("GameSystem");

        if (GameSystem == null)
        {
            GameSystem = new GameObject { name = "GameSystem" };

            Managers.Game.monsterSpawner = GameSystem.AddComponent<MonsterSpawner>();
            Managers.Game.inGameTimer = GameSystem.AddComponent<InGameTimer>();
        }

        Util.GetMonoBehaviour().StartCoroutine(DataLoading());
    }
    public async void LoadUserLevelInfo()
    {
        userLevelInfo = await Util.LoadingToPath<UserLevelInfo_SO>(userLevelInfoPath);
    }
    public IEnumerator DataLoading()
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

        Managers.Game.objectPool.CreateInstance(monsterList, defaultMonsterCount, true);
        Managers.Game.objectPool.CreateInstance(skillList, defaultSkillCount, true);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => typeCount == Managers.Game.objectPool.PoolingObjectsCount);

        yield return new WaitUntil(() => typeCount == Managers.Game.objectPool.ScriptableObjectsCount);

        yield return new WaitUntil(() => Managers.Game.player != null);

        Managers.Game.player.Set();

        Managers.Game.GameStart();
    }
}