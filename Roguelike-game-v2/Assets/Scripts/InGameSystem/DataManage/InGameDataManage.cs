using System;
using UnityEngine;
public class InGameDataManage
{
    public Action OptionCountUpdate = null;
    public PlayerData playerData = new();
    public AttackData attack = new();
    public DataInit dataInit = new();

    private const int maxOptionCount = 5;

    private int optionCount = 1;// = 3;
    
    public int OptionCount
    {
        get
        {
            return Mathf.Min(optionCount, maxOptionCount);
        }
        set
        {
            optionCount = value;

            if(OptionCountUpdate != null)
            {
                OptionCountUpdate.Invoke();
            }
        }
    }
}