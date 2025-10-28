using System;
using UnityEngine;
/// <summary>
/// skill option ���� ������Ʈ action�� Player, Skill ���� �� �ʱ�ȭ
/// </summary>
public class InGameData_Manage
{
    public Action skillOptionCount_Update = null;
    public PlayerData player = new();
    public SkillDatas skill = new();

    private const int maxOptionCount = 5;

    private int optionCount = 3;
    
    public int OptionCount
    {
        get { return Mathf.Min(optionCount, maxOptionCount); }
        set
        {
            optionCount = value;

            if(skillOptionCount_Update != null)
            {
                skillOptionCount_Update.Invoke();
            }
        }
    }
}