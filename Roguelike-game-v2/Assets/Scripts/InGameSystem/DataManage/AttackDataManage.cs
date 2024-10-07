using System;
using System.Collections.Generic;
public class AttackDataManage
{
    private Dictionary<string, (AttackInformation_SO, Action<int>, int)> attackData = new();//List?

    public Dictionary<string, (AttackInformation_SO, Action<int>, int)> AttackData { get { return attackData; } set { attackData = value; } }//Dictionary?
    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(!attackData.ContainsKey(so.attackName))
        {
            attackData.Add(so.attackName, (so, null, 0));
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetAttackData(key, out (AttackInformation_SO, Action<int>, int) data))
        {
            data.Item3 += levelDelta;

            data.Item2.Invoke(data.Item3);
        }
    }
    public (AttackInformation_SO, int) GetAttackData(string key)
    {
        if(TryGetAttackData(key, out (AttackInformation_SO, Action<int>, int) data))
        {
            return (data.Item1, data.Item3);
        }

        return (null, 0);
    }
    public AttackInformation_SO GetAttackInformation(string key)
    {
        if(TryGetAttackData(key, out (AttackInformation_SO, Action<int>, int) data))
        {
            return data.Item1;
        }

        return null;
    }
    public int GetAttackLevel(string key)
    {
        if(TryGetAttackData(key, out (AttackInformation_SO, Action<int>, int) data))
        {
            return data.Item3;
        }

        return 0;
    }
    public List<(AttackInformation_SO, Action<int>, int)> GetRandomAttackInformation()//Managers.Game.inGameData.OptionCount
    {
        List<(AttackInformation_SO, Action<int>, int)> attackDataList = new();

        foreach(int index in Calculate.GetRandomValues())
        {
            attackDataList.Add(attackData);

            attackDataList.Remove(attackData[index]);
        }

        return attackDataList;
    }
    private bool TryGetAttackData(string key, out (AttackInformation_SO, Action<int>, int) data)
    {
        if (attackData.ContainsKey(key))
        {
            data = attackData[key];

            return true;
        }
        else
        {
            data = (null, null, 0);

            return false;
        }
    }
}