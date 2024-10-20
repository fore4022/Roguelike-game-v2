using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class AttackSelection_UI : UserInterface
{
    private List<(int spacingX, int spacingY)> gridLayoutValues = new() { (365, 140), (250,225), (150,65)};

    private List<AttackOption_UI> attackOptionList = new();

    private GridLayoutGroup gridLayoutGroup = null;

    private void OnEnable()
    {
        if(attackOptionList == null)
        {
            return;
        }

        Set();
    }
    protected override void Start()
    {
        base.Start();

        Init();

        gameObject.SetActive(false);
    }
    private void Set()
    {
        if(gridLayoutGroup == null)
        {
            return;
        }

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
    private IEnumerator Init()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        yield return new WaitUntil(() => Managers.Game.inGameData != null);

        //Managers.UI.CreateUI<AttackOption_UI>();

        foreach (Transform transform in GetComponentInChildren<Transform>())//
        {
            if (transform != this.transform)
            {
                if (transform.gameObject.TryGetComponent<AttackOption_UI>(out AttackOption_UI attackOption_UI))
                {
                    attackOptionList.Add(attackOption_UI);
                }

                transform.gameObject.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        foreach(AttackOption_UI attackOption in attackOptionList)
        {
            GameObject go = attackOption.gameObject;

            if(go.activeSelf)
            {
                go.SetActive(false);
            }
            else
            {
                break;
            }
        }
    }
}