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
    private Image backGroundImage;
    private GameObject attackOption = null;

    private const int spacingY = 75;

    private (int x, int y) cellSize = (700, 255);

    protected override void Awake()
    {
        base.Awake();

        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        backGroundImage = GetComponent<Image>();
    }
    private void OnEnable()
    {
        if (attackOptionList.Count == 0)
        {
            return;
        }

        InputActions.DisableInputAction<TouchControls>();

        Set();
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    private void Set()
    {
        int optionCount = Managers.Game.inGameData.OptionCount - 3;

        int[] DataIndexArray = Managers.Game.calculate.GetRandomValues(Managers.Game.inGameData.attackData.attackInfo.Count, Managers.Game.inGameData.OptionCount);

        for(int i = 0; i < DataIndexArray.Count(); i++)
        {
            attackOptionList[i].gameObject.SetActive(true);
            attackOptionList[i].InitOption(DataIndexArray[i]);
        }

        backGroundImage.enabled = true;
    }
    private void AdjustGridLayout()
    {
        gridLayoutGroup.cellSize = new Vector2(cellSize.x, cellSize.y);
        gridLayoutGroup.spacing = new Vector2(0, spacingY);
    }
    private void OnDisable()
    {
        foreach (AttackOption_UI attackOption in attackOptionList)
        {
            if (attackOption.enabled == true)
            {
                break;
            }

            attackOption.gameObject.SetActive(false);
        }

        backGroundImage.enabled = false;

        Time.timeScale = 1;
    }
    private async void LoadAttackOption()
    {
        string path = Managers.UI.GetName<AttackOption_UI>();

        attackOption = await Util.LoadingToPath<GameObject>(path);
    }
    private IEnumerator Init()
    {
        LoadAttackOption();

        AdjustGridLayout();

        backGroundImage.enabled = false;

        yield return new WaitUntil(() => attackOption != null);
        
        yield return new WaitUntil(() => Managers.Game.inGameData != null);

        for (int i = 0; i < Managers.Game.inGameData.OptionCount; i++)
        {
            GameObject go = Instantiate(attackOption, transform);

            AttackOption_UI component = go.GetComponent<AttackOption_UI>();

            attackOptionList.Add(component);

            go.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}