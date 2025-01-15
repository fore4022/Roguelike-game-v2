using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataInit
{
    public UserLevelInfo_SO userLevelInfo;
    public ObjectPool objectPool = null;

    private const string userLevelInfoPath = "UserLevelInfoSO";
    private const string inGameScene = "InGame";
    private const int defaultMonsterCount = 100;
    private const int defaultSkillCount = 30;

    public void GetInGameData()
    {
        Util.GetMonoBehaviour().StartCoroutine(Init());
    }
    public void GetMonsterList(ref List<GameObject> monsterList)
    {
        foreach (SpawnInformation_SO so in Managers.Game.stageInformation.spawnInformationList)
        {
            foreach (SpawnInformation info in so.monsterInformation)
            {
                monsterList.Add(info.monster);
            }
        }
    }
    public void LoadSkillList(ref List<GameObject> skillList)
    {
        for(int i = 0; i < Managers.UserData.GetUserData.userLevel; i++)
        {
            UserLevel_SO userLevel = userLevelInfo.LevelInfo[i];

            foreach(AttackInformation_SO so in userLevel.attackInformationList)
            {
                skillList.Add(so.skillObject);

                Managers.Game.inGameData.attackData.SetDictionaryItem(so);
            }
        }
    }
    private IEnumerator Init()
    {
        Time.timeScale = 0;

        Managers.Scene.LoadScene(inGameScene);

        yield return new WaitUntil(() => inGameScene == Managers.Scene.CurrentScene);

        Managers.UI.CreateAndExcute<SceneLoading_UI>(new Action(Managers.UI.GetUI<SceneLoading_UI>().PlayAnimation));

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

        yield return new WaitUntil(() => Managers.Game.inGameData.playerData.levelUpdate != null);

        yield return new WaitUntil(() => typeCount == Managers.Game.inGameData.dataInit.objectPool.PoolingObjectsCount);

        yield return new WaitUntil(() => typeCount == Managers.Game.inGameData.dataInit.objectPool.ScriptableObjectsCount);

        yield return new WaitUntil(() => Managers.UI.IsInitalize == true);

        yield return new WaitUntil(() => Managers.Game.player != null);

        Managers.UI.GetUI<SceneLoading_UI>().IsLoading = false;

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() == null);

        Managers.Game.GameStart();
    }
}