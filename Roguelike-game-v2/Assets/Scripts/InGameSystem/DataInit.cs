using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataInit
{
    private const string userLevelPath = "_Level";
    private const int defaultMonsterCount = 300;
    private const int defaultSkillCount = 20;

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
        SkillInformation_SO so;
        GameObject skill;

        for(int i = 1; i <= Managers.UserData.data.Level; i++)
        {
            userLevel = Util.LoadingToPath<UserLevel_SO>($"{i}{userLevelPath}");

            foreach(string path in userLevel.pathList)
            {
                so = Util.LoadingToPath<SkillInformation_SO>(path);
                skill = Util.LoadingToPath<GameObject>(so.info.type);
                
                skillList.Add(skill);

                Managers.Game.inGameData.skill.SetDictionaryItem(so);
            }
        }
    }
    public IEnumerator Initializing()
    {
        Time.timeScale = 0;
        Managers.Game.IsPlaying = false;

        Managers.Scene.LoadScene(SceneName.InGame);

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Managers.Scene.IsSceneLoadComplete);

        Managers.Game.objectPool = new();

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

            Managers.Game._bgm = gameSystem.AddComponent<AudioSource>();
            Managers.Game.monsterSpawner = gameSystem.AddComponent<MonsterSpawner>();
            Managers.Game.inGameTimer = gameSystem.AddComponent<InGameTimer>();

            Managers.Game._bgm.playOnAwake = true;

            Managers.Audio.Registration(Managers.Game._bgm, SoundTypes.BGM);
        }
        else
        {
            Managers.Game._bgm = gameSystem.GetComponent<AudioSource>();
        }

        Managers.Game._bgm.clip = Managers.Game.stageInformation.bgm;
        Time.timeScale = 0;

        GetMonsterList(ref monsterList);
        LoadSkillList(ref skillList);

        yield return new WaitUntil(() => (skillList != null) && (monsterList != null));

        Managers.Game.monsterSpawner.monsterList = monsterList;

        Managers.Game.objectPool.Create(monsterList, ScriptableObjectType.Monster,defaultMonsterCount);
        Managers.Game.objectPool.Create(skillList, ScriptableObjectType.Skill, defaultSkillCount);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        yield return new WaitUntil(() => Managers.Game.inGameData.player.levelUpdate != null);

        yield return new WaitUntil(() => typeCount <= Managers.Game.objectPool.PoolingObjectsCount);

        yield return new WaitUntil(() => typeCount <= Managers.Game.objectPool.ScriptableObjectsCount);

        yield return new WaitUntil(() => Managers.Game.player != null);

        Object.Instantiate(Util.LoadingToPath<GameObject>(Managers.Main.GetCurrentStage().stagePath));

        Managers.UI.Get<SceneLoading_UI>().Wait = false;

        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() == null);

        Managers.Game._bgm.Play();
        Managers.Game.GameStart();
    }
}