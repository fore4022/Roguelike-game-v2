using System.Collections.Generic;
using System.Linq;
public class SkillContext
{
    public SkillInformation data;
    public SkillCaster caster = null;
    
    public int level = 0;

    public SkillContext(SkillInformation_SO dataSO)
    {
        data = new(dataSO);
    }
}
public class SkillData
{
    private Dictionary<string, SkillContext> info = new();

    public void SetDictionaryItem(SkillInformation_SO so)
    {
        if(!info.ContainsKey(so.info.type))
        {
            info.Add(so.info.type, new SkillContext(so));
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetAttackData(key, out SkillContext info))
        {
            if(info.caster == null)
            {
                info.caster = Managers.Game.skillCasterManage.CreateAndGetCaster(key);
                info.caster.Level = levelDelta - 1;
            }
            else
            {
                info.level += levelDelta;

                Managers.Game.skillCasterManage.UpdateCasterLevel(key, info.level);
            }
        }
    }
    public int GetLevel(string key)
    {
        if(TryGetAttackData(key, out SkillContext info))
        {
            return info.level;
        }

        return -1;
    }
    public List<SkillContext> GetAttackInformation()
    {
        List<SkillContext> info = this.info.Values.ToList();

        info.RemoveAll(o => o.level == Skill_SO.maxLevel - 1);

        return info;
    }
    private bool TryGetAttackData(string key, out SkillContext info)
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