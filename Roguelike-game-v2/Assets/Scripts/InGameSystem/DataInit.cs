using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 게임 정보 불러오기 및 시스템 초기화
/// </summary>
public class DataInit
{
    private List<GameObject> skillList = new();
    private GameObject damageText;
    private GameObject stage;

    private const string userLevelPath = "_Level";
    private const int defaultMonsterCount = 325;
    private const int defaultSkillCount = 40;

    private async Task LoadSkillList()
    {
        UserLevel_SO userLevel;
        SkillInformation_SO so;
        GameObject skill;

        for(int i = 1; i <= Managers.Data.user.Level; i++)
        {
            userLevel = await Addressable_Helper.LoadingToPath<UserLevel_SO>($"{i}{userLevelPath}");

            foreach(string path in userLevel.pathList)
            {
                so = await Addressable_Helper.LoadingToPath<SkillInformation_SO>(path);
                skill = await Addressable_Helper.LoadingToPath<GameObject>(so.info.type);

                skillList.Add(skill);
                Managers.Game.inGameData_Manage.skill.SetDictionaryItem(so);
            }
        }
    }
    private async Task LoadDamageText()
    {
        damageText = await Addressable_Helper.LoadingToPath<GameObject>(DamageLog_Manage.prefabName);
    }
    private async Task LoadStage()
    {
        stage = await Addressable_Helper.LoadingToPath<GameObject>(Managers.Main.GetCurrentStageSO().stagePath);
    }
    public IEnumerator Initializing()
    {
        Time.timeScale = 0;
        Managers.Game.Playing = false;

        Managers.Scene.LoadScene(SceneName.InGame);

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => Managers.Scene.IsLoading);

        Managers.Game.objectPool = new();

        CoroutineHelper.StartCoroutine(DataLoading());
    }
    private IEnumerator DataLoading()
    {
        List<GameObject> monsterList;

        Time.timeScale = 0;
        monsterList = Managers.Game.stageInformation.spawnMonsterList.monsters;

        Task loadStage = LoadStage();
        Task loadSkill =  LoadSkillList();
        Task loadDamageText = LoadDamageText();

        yield return new WaitUntil(() => loadStage.IsCompleted && loadSkill.IsCompleted && loadDamageText.IsCompleted);

        Object.Instantiate(stage);

        Managers.Game.monsterSpawner.monsterList = monsterList;
        Managers.Game.inGameData_Manage.player.MaxLevel = skillList.Count;

        Managers.Game.objectPool.Create(monsterList, ScriptableObjectType.Monster,defaultMonsterCount);
        Managers.Game.objectPool.Create(skillList, ScriptableObjectType.Skill, defaultSkillCount);
        Managers.Game.objectPool.Create(damageText, ScriptableObjectType.None);

        int typeCount = monsterList.Count + skillList.Count;

        yield return new WaitUntil(() => Managers.UI.IsInitalized());

        yield return new WaitUntil(() => Managers.Game.inGameData_Manage.player.levelUpdate != null);

        yield return new WaitUntil(() => typeCount + 1 <= Managers.Game.objectPool.PoolingObjectsCount);

        Managers.Game.damageLog_Manage.Set();

        yield return new WaitUntil(() => typeCount <= Managers.Game.objectPool.ScriptableObjectsCount);

        yield return new WaitUntil(() => Managers.Game.damageLog_Manage.isSet);

        yield return new WaitUntil(() => Managers.Game.player != null);

        Managers.UI.Get<SceneLoading_UI>().Wait = false;

        yield return new WaitUntil(() => Managers.UI.Get<SceneLoading_UI>() == null);

        Managers.Game.GameStart();
    }
}