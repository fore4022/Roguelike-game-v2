using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SkillSelection_UI : UserInterface
{
    private List<SkillOption_UI> skillOptionList = new();
    private Image background;
    private GameObject attackOption = null;

    private const string path = "SkillOption";
    private const float duration = 0.75f;
    private const float basicAlpha = 180;
    private const int targetAlpha = 0;

    private bool isSelect = true;

    public int RequireOptionCount { get { return Managers.Game.inGameData_Manage.OptionCount - skillOptionList.Count; } }
    public bool IsSelect { get { return isSelect; } set { isSelect = value; } }
    public override async void SetUserInterface()
    {
        background = GetComponent<Image>();
        attackOption = await Util.LoadingToPath<GameObject>(path);

        Managers.Game.inGameData_Manage.player.levelUpdate += () => Managers.UI.Show<LevelUp_UI>();

        StartCoroutine(Init());
    }
    public void SkillOptionToggle(bool active)
    {
        foreach(SkillOption_UI attackOption in skillOptionList)
        {
            attackOption.gameObject.SetActive(active);
        }

        background.enabled = active;
    }
    protected override void Enable()
    {
        Managers.UI.Show<SkillPoints_UI>();

        Set();
    }
    public void Set()
    {
        Managers.UI.Get<SkillPoints_UI>().SkillPointsUpdate();

        if(skillOptionList.Count == 0)
        {
            return;
        }

        IsSelect = false;

        StartCoroutine(Setting());
    }
    public void Selected()
    {
        foreach(SkillOption_UI attackOption in skillOptionList)
        {
            attackOption.gameObject.SetActive(false);
        }

        Managers.Game.inGameData_Manage.player.LevelUpCount--;
        isSelect = true;

        StartCoroutine(SkillListUpdate());
    }
    private void OnDisable()
    {
        foreach(SkillOption_UI attackOption in skillOptionList)
        {
            if(attackOption.enabled == true)
            {
                break;
            }

            attackOption.Reset();
            attackOption.gameObject.SetActive(false);
        }

        background.enabled = false;

        InputActions.EnableInputAction<TouchControls>();
    }
    private void CreateOptionUI()
    {
        Transform trans = transform.GetChild(0);
        GameObject go;

        int count = RequireOptionCount;

        for (int i = 0; i < count; i++)
        {
            go = Instantiate(attackOption, trans);

            skillOptionList.Add(Util.GetComponentInChildren<SkillOption_UI>(go.transform));

            go.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private IEnumerator Init()
    {
        background.enabled = false;

        yield return new WaitUntil(() => Managers.Game.inGameData_Manage != null);
        
        yield return new WaitUntil(() => attackOption != null);

        CreateOptionUI();

        Managers.Game.inGameData_Manage.skillOptionCount_Update += CreateOptionUI;

        gameObject.SetActive(false);
    }
    private IEnumerator Setting()
    {
        List<SkillContext> infoList = Managers.Game.inGameData_Manage.skill.GetAttackInformation();

        int[] indexArray = Calculate.GetRandomValues(infoList.Count, Mathf.Min(Managers.Game.inGameData_Manage.OptionCount, infoList.Count));

        UIElementUtility.SetImageAlpha(background, basicAlpha);

        yield return new WaitForEndOfFrame();

        for(int i = 0; i < indexArray.Count(); i++)
        {
            skillOptionList[i].gameObject.SetActive(true);
            skillOptionList[i].InitOption(infoList[indexArray[i]]);
        }

        background.enabled = true;
    }
    private IEnumerator PadeIn()
    {
        Managers.UI.Hide<SkillPoints_UI>();
        UIElementUtility.SetImageAlpha(background, targetAlpha, duration);

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
        Managers.Game.Playing = true;

        InputActions.EnableInputAction<TouchControls>();
        Managers.UI.Hide<SkillSelection_UI>();
    }
    private IEnumerator SkillListUpdate()
    {
        while(Managers.Game.inGameData_Manage.player.LevelUpCount > 0)
        {
            Set();

            yield return new WaitUntil(() => IsSelect);
        }

        StartCoroutine(PadeIn());
    }
}