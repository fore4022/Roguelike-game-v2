using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataInit
{
    public UserLevels_SO userLevelInfo;
    public ObjectPool objectPool = null;

    private const string userLevelsPath = "UserLevels_SO";
    private const int defaultMonsterCount = 100;
    private const int defaultSkillCount = 30;

    public void GetInGameData(bool isReStart)
    {
        Util.GetMonoBehaviour().StartCoroutine(Init(isReStart));
    }
    public void GetMonsterList(ref List<GameObject> monsterList)
    {
        foreach(SpawnInformation_SO so in Managers.Game.stageInformation.spawnInformationList)
        {
            foreach(SpawnInformation info in so.monsterInformation)
            {
                if(!monsterList.Contains(info.monster))
                {
                    monsterList.Add(info.monster);
                }
            }
        }
    }
    public void LoadSkillList(ref List<GameObject> skillList)
    {
        for(int i = 0; i < Managers.UserData.data.Level; i++)
        {
            UserLevel_SO userLevel = userLevelInfo.LevelInfo[i];

            foreach(AttackInformation_SO so in userLevel.attackInformationList)
            {
                skillList.Add(so.skillObject);

                Managers.Game.inGameData.attack.SetDictionaryItem(so);
            }
        }
    }
    private async void LoadUserLevels()
    {
        userLevelInfo = await Util.LoadingToPath<UserLevels_SO>(userLevelsPath);
    }
    private IEnumerator Init(bool isReStart)
    {
        Time.timeScale = 0;

        if(!isReStart)
        {
            Managers.Scene.LoadScene(Define.SceneName.InGame);
        }
        else
        {
            Managers.Scene.ReLoadScene();
        }

        yield return new WaitUntil(() => Managers.Scene.IsSceneLoadComplete);
        
        GameObject gameSystem = GameObject.Find("GameSystem");

        objectPool = new();

        if(gameSystem == null)
        {
            gameSystem = new GameObject { name = "GameSystem" };

            Managers.Game.monsterSpawner = gameSystem.AddComponent<MonsterSpawner>();
            Managers.Game.inGameTimer = gameSystem.AddComponent<InGameTimer>();
        }

        Util.GetMonoBehaviour().StartCoroutine(DataLoading());
    }
    private IEnumerator DataLoading()
    {
        List<GameObject> monsterList = new();
        List<GameObject> skillList = new();

        Time.timeScale = 0;
        
        LoadUserLevels();
            
        yield return new WaitUntil(() => userLevelInfo != null);

        GetMonsterList(ref monsterList);
        LoadSkillList(ref skillList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        Managers.Game.monsterSpawner.monsterList = monsterList;

        Managers.Game.inGameData.init.objectPool.CreateObjects(monsterList, defaultMonsterCount);
        Managers.Game.inGameData.init.objectPool.CreateObjects(skillList, defaultSkillCount);

        int typeCount = monsterList.Count + skillList.Count;

        Managers.UI.InitUI();

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        yield return new WaitUntil(() => Managers.Game.inGameData.player.levelUpdate != null);

        yield return new WaitUntil(() => typeCount == Managers.Game.inGameData.init.objectPool.PoolingObjectsCount);

        yield return new WaitUntil(() => typeCount == Managers.Game.inGameData.init.objectPool.ScriptableObjectsCount);
        
        yield return new WaitUntil(() => Managers.Game.player != null);

        Managers.UI.GetUI<SceneLoading_UI>().Wait = false;

        yield return new WaitUntil(() => Managers.UI.GetUI<SceneLoading_UI>() == null);

        Managers.Game.GameStart();
    }
}