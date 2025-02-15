using System;
using UnityEngine;
public class InGameDataManage
{
    public Action optionCountUpdate = null;
    public PlayerData player = new();
    public AttackData attack = new();
    public DataInit init = new();

    private const int maxOptionCount = 5;

    private int optionCount = 3;
    
    public int OptionCount
    {
        get { return Mathf.Min(optionCount, maxOptionCount); }
        set
        {
            optionCount = value;

            if(optionCountUpdate != null)
            {
                optionCountUpdate.Invoke();
            }
        }
    }
}