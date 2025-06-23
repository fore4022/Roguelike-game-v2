using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GridLayoutGroup))]
public class SkillSelection_UI : UserInterface
{
    private List<SkillOption_UI> skillOptionList = new();

    private GridLayoutGroup gridLayoutGroup = null;
    private Image background;
    private GameObject attackOption = null;

    private const string path = "SkillOption";
    private const float duration = 0.75f;
    private const float basicAlpha = 180;
    private const float targetAlpha = 0;
    private const int spacingY = 75;

    private (int x, int y) cellSize = (700, 255);
    private Coroutine levelUp = null;
    private bool isSelect = false;
    
    public override void SetUserInterface()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        background = GetComponent<Image>();

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
        Set();
    }
    public void Set()
    {
        if(skillOptionList.Count == 0)
        {
            return;
        }

        isSelect = false;

        StartCoroutine(Setting());
    }
    public void Selected()
    {
        foreach(SkillOption_UI attackOption in skillOptionList)
        {
            attackOption.gameObject.SetActive(false);
        }

        isSelect = true;

        a++;

        Debug.Log(a);
        Debug.Log(Managers.Game.inGameData.player.Level);
        Debug.Log("---");

        StartCoroutine(PadeIn());
    }
    private int a = 0;
    private void AdjustGridLayout()
    {
        gridLayoutGroup.cellSize = new Vector2(cellSize.x, cellSize.y);
        gridLayoutGroup.spacing = new Vector2(0, spacingY);
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
    private void LoadAttackOption()
    {
        attackOption = Util.LoadingToPath<GameObject>(path);
    }
    private void CreateOptionUI()
    {
        int count = Managers.Game.inGameData.OptionCount - skillOptionList.Count;

        for(int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(attackOption, transform);

            skillOptionList.Add(go.GetComponent<SkillOption_UI>());

            go.SetActive(false);
        }
    }
    private IEnumerator Init()
    {
        LoadAttackOption();
        AdjustGridLayout();

        background.enabled = false;

        yield return new WaitUntil(() => Managers.Game.inGameData != null);
        
        yield return new WaitUntil(() => attackOption != null);

        CreateOptionUI();

        Managers.Game.inGameData.optionCountUpdate += CreateOptionUI;

        gameObject.SetActive(false);
    }
    private IEnumerator Setting()
    {
        List<SkillContext> infoList = Managers.Game.inGameData.skill.GetAttackInformation();

        int[] indexArray = Calculate.GetRandomValues(infoList.Count, Mathf.Min(Managers.Game.inGameData.OptionCount, infoList.Count));

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
        if(Managers.Game.inGameData.player.LevelUpCount == 0)
        {
            Managers.UI.Show<HeadUpDisplay_UI>();
            UIElementUtility.SetImageAlpha(background, targetAlpha, duration);

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = 1;
            Managers.Game.IsPlaying = true;

            if(levelUp != null)
            {
                StopCoroutine(levelUp);
            }

            InputActions.EnableInputAction<TouchControls>();
            Managers.UI.Hide<SkillSelection_UI>();
        }
        else
        {
            if(levelUp == null)
            {
                levelUp = StartCoroutine(Managers.Game.inGameData.player.LevelUp());
            }
        }
    }
}