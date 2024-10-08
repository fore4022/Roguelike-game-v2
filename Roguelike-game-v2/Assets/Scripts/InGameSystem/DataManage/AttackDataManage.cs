using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
public class AttackDataManage
{
    private Dictionary<string, int> attackIndexMap = new();
    private List<(AttackInformation_SO, Action<int>, int)> attackData = new();

    private int totalIndex = 0;

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        Debug.Log(!attackIndexMap.ContainsKey(so.attackName));

        if(!attackIndexMap.ContainsKey(so.attackName))
        {
            attackIndexMap.Add(so.attackName, totalIndex);

            totalIndex++;
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

        foreach (int index in Calculate.GetRandomValues(attackData.Count, Managers.Game.inGameData.OptionCount))
        {
            attackDataList.Add(attackData[index]);
        }

        return attackDataList;
    }
    private bool TryGetAttackData(string key, out (AttackInformation_SO, Action<int>, int) data)
    {
        if (attackIndexMap.TryGetValue(key, out int index))
        {
            data = attackData[index];

            return true;
        }
        else
        {
            data = (null, null, 0);

            return false;
        }
    }
}