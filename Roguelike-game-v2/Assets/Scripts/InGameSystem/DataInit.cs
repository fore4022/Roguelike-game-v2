using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataInit
{
    public ObjectPool objectPool = null;

    private const string userLevelPath = "_Level_SO";
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
        UserLevel_SO userLevel;

        for(int i = 1; i <= Managers.UserData.data.Level; i++)
        {
            userLevel = Util.LoadingToPath<UserLevel_SO>(i + userLevelPath);

            foreach(AttackInformation_SO so in userLevel.attackInformationList)
            {
                skillList.Add(so.skillObject);

                Managers.Game.inGameData.attack.SetDictionaryItem(so);
            }
        }
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

        Util.GetMonoBehaviour().StartCoroutine(DataLoading());
    }
    private IEnumerator DataLoading()
    {
        List<GameObject> monsterList = new();
        List<GameObject> skillList = new();

        GameObject gameSystem = GameObject.Find("GameSystem");

        if(gameSystem == null)
        {
            gameSystem = new GameObject { name = "GameSystem" };

            Managers.Game.monsterSpawner = gameSystem.AddComponent<MonsterSpawner>();
            Managers.Game.inGameTimer = gameSystem.AddComponent<InGameTimer>();
        }

        Time.timeScale = 0;

        Debug.Log(SceneManager.GetActiveScene().name);

        objectPool = new();

        Debug.Log(SceneManager.GetActiveScene().name);
        GetMonsterList(ref monsterList);
        LoadSkillList(ref skillList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        Managers.Game.monsterSpawner.monsterList = monsterList;

        Managers.Game.inGameData.init.objectPool.CreateObjects(monsterList, defaultMonsterCount);
        Managers.Game.inGameData.init.objectPool.CreateObjects(skillList, defaultSkillCount);

        int typeCount = monsterList.Count + skillList.Count;

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