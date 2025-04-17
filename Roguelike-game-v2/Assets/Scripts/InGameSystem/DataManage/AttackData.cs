using System.Collections.Generic;
using System.Linq;
public class AttackContext
{
    public AttackInformation data;
    public AttackCaster caster = null;
    
    public int level = 0;

    public AttackContext(AttackInformation_SO dataSO)
    {
        data = new(dataSO);
    }
}
public class AttackData
{
    private Dictionary<string, AttackContext> info = new();

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(!info.ContainsKey(so.attackInfo.type))
        {
            info.Add(so.attackInfo.type, new AttackContext(so));
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetAttackData(key, out AttackContext info))
        {
            if(info.caster == null)
            {
                info.caster = Managers.Game.attackCasterManage.CreateAndGetCaster(key);
                info.caster.Level = levelDelta - 1;
            }
            else
            {
                info.level += levelDelta;

                Managers.Game.attackCasterManage.UpdateCasterLevel(key, info.level);
            }
        }
    }
    public int GetLevel(string key)
    {
        if(TryGetAttackData(key, out AttackContext info))
        {
            return info.level;
        }

        return -1;
    }
    public List<AttackContext> GetAttackInformation()
    {
        List<AttackContext> info = this.info.Values.ToList();

        info.RemoveAll(o => o.level == Attack_SO.maxLevel - 1);

        return info;
    }
    private bool TryGetAttackData(string key, out AttackContext info)
    {
        if(this.info.ContainsKey(key))
        {
            info = this.info[key];

            return true;
        }
        else
        {
            info = null;

            return false;
        }
    }
}