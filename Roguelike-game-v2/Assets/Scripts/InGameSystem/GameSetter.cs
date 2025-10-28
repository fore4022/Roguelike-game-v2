using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// ���� ���� �ҷ����� �� �ý��� �ʱ�ȭ
/// </summary>
public class GameSetter
{
    private PoolingObject_Initializer poolingObject_Initializer = new();

    private List<GameObject> skillList = new();
    private List<GameObject> monsterList;
    private GameObject damageText;
    private GameObject stage;

    private const string userLevelPath = "_Level";
    private const int defaultMonsterCount = 325;
    private const int defaultSkillCount = 40;

    private Coroutine coroutine = null;

    private async Task LoadSkillList()
    {
        UserLevel_SO userLevel;
        SkillInformation_SO so;
        GameObject skill;

        for(int i = 1; i <= Managers.Data.user.Level; i++)
        {
            userLevel = await AddressableHelper.LoadingToPath<UserLevel_SO>($"{i}{userLevelPath}");

            foreach(string path in userLevel.pathList)
            {
                so = await AddressableHelper.LoadingToPath<SkillInformation_SO>(path);
                skill = await AddressableHelper.LoadingToPath<GameObject>(so.info.type);

                skillList.Add(skill);
                Managers.Game.inGameData_Manage.skill.SetDictionaryItem(so);
            }
        }
    }
    private async Task LoadDamageText()
    {
        damageText = await AddressableHelper.LoadingToPath<GameObject>(DamageLog_Manage.prefabName);
    }
    private async Task LoadStage()
    {
        stage = await AddressableHelper.LoadingToPath<GameObject>(Managers.Main.GetCurrentStageSO().stagePath);
    }
    public IEnumerator Initializing()
    {
        Time.timeScale = 0;

        Managers.Scene.LoadScene(SceneNames.InGame);

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Managers.Scene.IsLoading);

        Managers.Game.objectPool = new();

        CoroutineHelper.StartCoroutine(Setting());
    }
    private IEnumerator Setting()
    {
        Time.timeScale = 0;

        coroutine = CoroutineHelper.StartCoroutine(DataLoading());

        yield return new WaitUntil(() => coroutine == null);

        coroutine = CoroutineHelper.StartCoroutine(InstantiateCreating());

        yield return new WaitUntil(() => coroutine == null);

        Managers.Audio.InitializedAudio();

        yield return new WaitUntil(() => Managers.Game.inGameData_Manage.player.levelUpdate != null);

        yield return new WaitUntil(() => Managers.Game.player != null);

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        Managers.UI.Get<SceneLoading_UI>().Wait = false;

        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() == null);

        Managers.Game.GameStart();
    }
    private IEnumerator DataLoading()
    {
        monsterList = Managers.Game.stageInformation.spawnMonsterList.monsters;

        Task loadStage = LoadStage();
        Task loadSkill = LoadSkillList();
        Task loadDamageText = LoadDamageText();

        yield return new WaitUntil(() => loadStage.IsCompleted && loadSkill.IsCompleted && loadDamageText.IsCompleted);

        Managers.Game.monsterSpawner.monsterList = monsterList;
        Managers.Game.inGameData_Manage.player.MaxLevel = skillList.Count;
        coroutine = null;
    }
    private IEnumerator InstantiateCreating()
    {
        Object.Instantiate(stage);

        Managers.Game.objectPool.Create(monsterList, defaultMonsterCount);
        Managers.Game.objectPool.Create(skillList, defaultSkillCount);
        Managers.Game.objectPool.Create(damageText);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => typeCount + 1 <= Managers.Game.objectPool.PoolingObjectsCount);

        poolingObject_Initializer.Start(monsterList, skillList);
        Managers.Game.damageLog_Manage.Set();

        yield return new WaitUntil(() => typeCount <= Managers.Game.so_Manage.ScriptableObjectsCount);

        yield return new WaitUntil(() => Managers.Game.damageLog_Manage.isSet);

        yield return new WaitUntil(() => poolingObject_Initializer.Init);

        coroutine = null;
    }
}