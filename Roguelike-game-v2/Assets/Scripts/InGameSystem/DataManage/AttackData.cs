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
    public Dictionary<string, int> attackIndexMap = new();
    public List<AttackInformation> attackInfo = new();

    private int totalIndex = 0;

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(!attackIndexMap.ContainsKey(so.attackType))
        {
            attackIndexMap.Add(so.attackType, totalIndex);
            attackInfo.Add(new AttackInformation(so));

            totalIndex++;
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
        if(attackIndexMap.TryGetValue(key, out int index))
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