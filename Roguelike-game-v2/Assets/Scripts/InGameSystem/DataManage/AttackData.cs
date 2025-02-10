using System.Collections.Generic;
public class AttackInformation
{
    public AttackInformation_SO data;
    public AttackCaster caster = null;
    
    public int level;

    public AttackInformation(AttackInformation_SO dataSO)
    {
        data = dataSO;
    }
}
public class AttackData
{
    public List<AttackInformation> infoList = new();

    private Dictionary<string, int> indexMap = new();

    private int total_Index = 0;

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(!indexMap.ContainsKey(so.attackType))
        {
            indexMap.Add(so.attackType, total_Index);
            infoList.Add(new AttackInformation(so));

            total_Index++;
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            if(info.caster == null)
            {
                info.caster = Managers.Game.attackCasterManage.CreateAndGetCaster(key);
            }
            else
            {
                info.level += levelDelta;

                Managers.Game.attackCasterManage.UpdateCasterLevel(key, info.level);

                if(info.level == Attack_SO.maxLevel - 1)
                {
                    infoList.Remove(info);
                }
            }
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
    public int GetLevel(string key)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            return info.level;
        }

        return 0;
    }
    private bool TryGetAttackData(string key, out AttackInformation info)
    {
        if(indexMap.TryGetValue(key, out int index))
        {
            info = infoList[index];

            return true;
        }
        else
        {
            info = null;

            return false;
        }
    }
}