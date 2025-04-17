using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataInit
{
    public ObjectPool objectPool = null;

    private const string userLevelPath = "_Level";
    private const int defaultMonsterCount = 100;
    private const int defaultSkillCount = 30;

    public void GetInGameData()
    {
        Util.GetMonoBehaviour().StartCoroutine(Init());
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
        AttackInformation_SO attackInfo;
        GameObject attack;

        for(int i = 1; i <= Managers.UserData.data.Level; i++)
        {
            userLevel = Util.LoadingToPath<UserLevel_SO>($"{i}{userLevelPath}");

            foreach(string path in userLevel.pathList)
            {
                attackInfo = Util.LoadingToPath<AttackInformation_SO>(path);
                attack = Util.LoadingToPath<GameObject>(attackInfo.attackInfo.type);
                
                skillList.Add(attack);

                Managers.Game.inGameData.attack.SetDictionaryItem(attackInfo);
            }
        }
    }
    private IEnumerator Init()
    {
        Time.timeScale = 0;

        Managers.Scene.LoadScene(Define.SceneName.InGame);

        yield return new WaitForEndOfFrame();

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
        objectPool = new();

        GetMonsterList(ref monsterList);
        LoadSkillList(ref skillList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        Managers.Game.monsterSpawner.monsterList = monsterList;

        Managers.Game.inGameData.init.objectPool.Create(monsterList, Define.ScriptableObjectType.Monster,defaultMonsterCount);
        Managers.Game.inGameData.init.objectPool.Create(skillList, Define.ScriptableObjectType.Attack, defaultSkillCount);

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