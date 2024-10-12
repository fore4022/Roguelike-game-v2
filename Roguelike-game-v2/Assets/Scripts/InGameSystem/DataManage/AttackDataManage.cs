using System;
using System.Collections.Generic;
public class AttackInformation
{
    public AttackInformation_SO data;

    [Obsolete]
    public Action<int> levelUpdate;//Class
    
    public int level;

    public AttackInformation(AttackInformation_SO dataSO)
    {
        data = dataSO;
    }
}
public class AttackDataManage
{
    public Dictionary<string, int> attackIndexMap = new();
    public List<AttackInformation> attackInfo = new();

    private int totalIndex = 0;

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(!attackIndexMap.ContainsKey(so.attackName))
        {
            attackIndexMap.Add(so.attackName, totalIndex);
            attackInfo.Add(new AttackInformation(so));

            totalIndex++;
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            info.level += levelDelta;

            info.levelUpdate.Invoke(info.level);
        }
    }
    public (AttackInformation_SO, int) GetAttackData(string key)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            return (info.data, info.level);
        }

        return (null, 0);
    }
    public AttackInformation_SO GetAttackInformation(string key)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            return info.data;
        }

        return null;
    }
    public int GetAttackLevel(string key)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            return info.level;
        }

        return 0;
    }
    private bool TryGetAttackData(string key, out AttackInformation info)
    {
        if (attackIndexMap.TryGetValue(key, out int index))
        {
            info = attackInfo[index];

            return true;
        }
        else
        {
            info = null;

            return false;
        }
    }
}