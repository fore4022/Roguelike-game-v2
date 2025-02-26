using System.Collections.Generic;
using System.Linq;
public class AttackInformation
{
    public AttackInformation_SO data;
    public AttackCaster caster = null;
    
    public int level = 0;

    public AttackInformation(AttackInformation_SO dataSO)
    {
        data = dataSO;
    }
}
public class AttackData
{
    private Dictionary<string, AttackInformation> info = new();

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(!info.ContainsKey(so.attackType))
        {
            info.Add(so.attackType, new AttackInformation(so));
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

                UnityEngine.Debug.Log(this.info[key].level);
            }
        }
    }
    public int GetLevel(string key)
    {
        if(TryGetAttackData(key, out AttackInformation info))
        {
            return info.level;
        }

        return -1;
    }
    public List<AttackInformation> GetAttackInformation()
    {
        List<AttackInformation> info = this.info.Values.ToList();

        info.RemoveAll(o => o.level == Attack_SO.maxLevel - 1);

        return info;
    }
    private bool TryGetAttackData(string key, out AttackInformation info)
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