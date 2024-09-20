using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InGameDataLoad
{
    public static UserLevelInfo_SO userLevelInfo;

    public static string userLevelInfoPath = "UserLevelInfoSO";
    public static string inGameScene = "InGame";
    public static bool LoadComplete = false;

    public static void GetInGameData()
    {
        Util.GetMonoBehaviour().StartCoroutine(Init());
    }
    public static void GetMonsterList(ref List<GameObject> monsterList)
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
    public static void LoadSkillList(ref List<GameObject> skillList)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < Managers.UserData.GetUserData.userLevel; i++)
        {
            UserLevel_SO userLevel = userLevelInfo.LevelInfo[i];

            foreach (GameObject prefab in userLevel.skillList)
            {
                list.Add(prefab);
            }
        }

        skillList = list;
    }
    public static IEnumerator Init()
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
    public static async void LoadUserLevelInfo()
    {
        userLevelInfo = await Util.LoadingToPath<UserLevelInfo_SO>(userLevelInfoPath);
    }
    public static IEnumerator DataLoading()
    {
        List<GameObject> monsterList = new();
        List<GameObject> skillList = new();

        Time.timeScale = 0;

        LoadUserLevelInfo();

        yield return new WaitUntil(() => userLevelInfo != null);

        GetMonsterList(ref monsterList);
        LoadSkillList(ref skillList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        ObjectPool.CreateInstance(monsterList);
        ObjectPool.CreateInstance(skillList);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => typeCount == ObjectPool.poolingObjects.Count);

        yield return new WaitUntil(() => typeCount == ObjectPool.scriptableObjects.Count);

        yield return new WaitUntil(() => Managers.Game.player != null);

        LoadComplete = true;

        Managers.Game.Set();

        Managers.Game.GameStart();
    }
}