using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GridLayoutGroup))]
public class AttackSelection_UI : UserInterface
{
    private List<(int spacingX, int spacingY)> gridLayoutValues = new() { (365, 140), (250,225), (150,65)};

    private List<AttackOption_UI> attackOptionList = new();

    private GridLayoutGroup gridLayoutGroup = null;
    private GameObject attackOption = null;

    private void OnEnable()
    {
        if(attackOptionList.Count == 0)
        {
            return;
        }

        Set();
    }
    protected override void Start()
    {
        base.Start();

        StartCoroutine(Init());
    }
    private void Set()
    {
        int optionCount = Managers.Game.inGameData.OptionCount - 3;

        AdjustGridLayout(optionCount);

        int[] DataIndexArray = Calculate.GetRandomValues(Managers.Game.inGameData.attackData.attackInfo.Count, Managers.Game.inGameData.OptionCount);

        for(int i = 0; i < DataIndexArray.Count(); i++)
        {
            attackOptionList[i].gameObject.SetActive(true);
            attackOptionList[i].InitOption(DataIndexArray[i]);
        }
    }
    private void AdjustGridLayout(int index)
    {
        gridLayoutGroup.spacing = new Vector2(gridLayoutValues[index].spacingX, gridLayoutValues[index].spacingY);
    }
    private void OnDisable()
    {
        foreach (AttackOption_UI attackOption in attackOptionList)
        {
            GameObject go = attackOption.gameObject;

            if (go.activeSelf)
            {
                go.SetActive(false);
            }
            else
            {
                break;
            }
        }
    }
    private async void LoadAttackOption()
    {
        string path = Managers.UI.GetName<AttackOption_UI>();

        attackOption = await Util.LoadingToPath<GameObject>(path);
    }
    private IEnumerator Init()
    {
        LoadAttackOption();

        gridLayoutGroup = GetComponent<GridLayoutGroup>();

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