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
        if (attackOptionList.Count == 0)
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
    private IEnumerator Set()
    {
        int optionCount = Managers.Game.inGameData.OptionCount - 3;

        int[] DataIndexArray = Managers.Game.calculate.GetRandomValues(2, 2);//Managers.Game.inGameData.attackData.attackInfo.Count, Managers.Game.inGameData.OptionCount

        Managers.UI.uiElementUtility.SetImageAlpha(background, basicAlpha);

        yield return null;

        for (int i = 0; i < DataIndexArray.Count(); i++)
        {
            attackOptionList[i].gameObject.SetActive(true);
            attackOptionList[i].InitOption(DataIndexArray[i]);
        }

        background.enabled = true;
    }
    private void AdjustGridLayout()
    {
        gridLayoutGroup.cellSize = new Vector2(cellSize.x, cellSize.y);
        gridLayoutGroup.spacing = new Vector2(0, spacingY);
    }
    public void Selected()
    {
        foreach(AttackOption_UI attackOption in attackOptionList)
        {
            attackOption.gameObject.SetActive(false);
        }

        StartCoroutine(PadeIn());
    }
    private void OnDisable()
    {
        foreach(AttackOption_UI attackOption in attackOptionList)
        {
            if (attackOption.enabled == true)
            {
                break;
            }

            attackOption.gameObject.SetActive(false);
        }

        background.enabled = false;

        InputActions.EnableInputAction<TouchControls>();
    }
    private async void LoadAttackOption()
    {
        attackOption = await Util.LoadingToPath<GameObject>(path);
    }
    private IEnumerator Init()
    {
        LoadAttackOption();

        AdjustGridLayout();

        background.enabled = false;

        yield return new WaitUntil(() => attackOption != null);
        
        yield return new WaitUntil(() => Managers.Game.inGameData != null);

        for (int i = 0; i < Managers.Game.inGameData.OptionCount; i++)
        {
            GameObject go = Instantiate(attackOption, transform);

            AttackOption_UI component = go.GetComponent<AttackOption_UI>();

            attackOptionList.Add(component);

            component.Set();

            go.SetActive(false);

            yield return null;
        }

        gameObject.SetActive(false);
    }
    private IEnumerator PadeIn()
    {
        Managers.UI.uiElementUtility.SetImageAlpha(background, targetAlpha, duration);

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1;

        InputActions.EnableInputAction<TouchControls>();

        Managers.UI.HideUI<AttackSelection_UI>();
    }
}