using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GridLayoutGroup))]
public class AttackSelection_UI : UserInterface
{
    private List<AttackOption_UI> attackOptionList = new();

    private GridLayoutGroup gridLayoutGroup = null;
    private Image background;
    private GameObject attackOption = null;

    private const string path = "AttackOption";
    private const float duration = 0.75f;
    private const float basicAlpha = 180;
    private const float targetAlpha = 0;
    private const int spacingY = 75;

    private (int x, int y) cellSize = (700, 255);

    protected override void Enable()
    {
        if(attackOptionList.Count == 0)
        {
            return;
        }

        StartCoroutine(Set());
    }
    public override void SetUserInterface()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        background = GetComponent<Image>();

        StartCoroutine(Init());
    }
    public void Selected()
    {
        foreach (AttackOption_UI attackOption in attackOptionList)
        {
            attackOption.gameObject.SetActive(false);
        }

        StartCoroutine(PadeIn());
    }
    private void AdjustGridLayout()
    {
        gridLayoutGroup.cellSize = new Vector2(cellSize.x, cellSize.y);
        gridLayoutGroup.spacing = new Vector2(0, spacingY);
    }
    private void OnDisable()
    {
        foreach(AttackOption_UI attackOption in attackOptionList)
        {
            if(attackOption.enabled == true)
            {
                break;
            }

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
        int count = Managers.Game.inGameData.OptionCount - attackOptionList.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(attackOption, transform);

            attackOptionList.Add(go.GetComponent<AttackOption_UI>());

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
    private IEnumerator Set()
    {
        List<AttackContext> infoList = Managers.Game.inGameData.attack.GetAttackInformation();

        int[] indexs = Calculate.GetRandomValues(infoList.Count, Mathf.Min(Managers.Game.inGameData.OptionCount, infoList.Count));

        UIElementUtility.SetImageAlpha(background, basicAlpha);

        yield return null;

        for (int i = 0; i < indexs.Count(); i++)
        {
            attackOptionList[i].gameObject.SetActive(true);
            attackOptionList[i].InitOption(infoList[indexs[i]]);
        }

        background.enabled = true;
    }
    private IEnumerator PadeIn()
    {
        UIElementUtility.SetImageAlpha(background, targetAlpha, duration);

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1;

        InputActions.EnableInputAction<TouchControls>();
        Managers.UI.HideUI<AttackSelection_UI>();
    }
}