using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class attackSelection_UI : MonoBehaviour
{
    [SerializeField]
    private List<(int spacingX, int spacingY)> gridLayoutValues = new() { (365, 140), (250,225), (150,65)};

    private List<AttackOption_UI> attackOptionList = new();

    private GridLayoutGroup gridLayoutGroup = null;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();

        foreach(Transform transform in GetComponentInChildren<Transform>())
        {
            if(transform != this.transform)
            {
                if(transform.gameObject.TryGetComponent<AttackOption_UI>(out AttackOption_UI attackOption_UI))
                {
                    attackOptionList.Add(attackOption_UI);
                }

                transform.gameObject.SetActive(false);
            }
        }
    }
    public void Set()
    {
        if(gridLayoutGroup == null)
        {
            return;
        }

        int index = Managers.Game.inGameData.OptionCount - 3;

        AdjustGridLayout(index);

        List<(AttackInformation_SO, Action<int>, int)> attackDataList = Managers.Game.inGameData.attackData.GetRandomAttackInformation();

        for (int i = 0; i < Mathf.Min(attackDataList.Count, Managers.Game.inGameData.OptionCount); i++)
        {
            attackOptionList[i].gameObject.SetActive(true);
            attackOptionList[i].InitOption(attackDataList[i]);
        }
    }
    private void AdjustGridLayout(int index)
    {
        gridLayoutGroup.spacing = new Vector2(gridLayoutValues[index].spacingX, gridLayoutValues[index].spacingY);
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