using System.Collections.Generic;
using System.Linq;
/// <summary>
/// ��ų�� ������ �����ϴ� ������ ������.
/// </summary>
public class SkillDatas
{
    private Dictionary<string, SkillContext> infos = new();

    public void Reset()
    {
        foreach(string key in infos.Keys)
        {
            infos[key].caster = null;
        }
    }
    public void SetDictionaryItem(SkillInformation_SO so)
    {
        if(!infos.ContainsKey(so.info.type))
        {
            infos.Add(so.info.type, new SkillContext(so));
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if(TryGetSkillData(key, out SkillContext info))
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
        if(TryGetSkillData(key, out SkillContext info))
        {
            return info.level;
        }

        return -1;
    }
    public List<SkillContext> GetAttackInformation()
    {
        List<SkillContext> info = infos.Values.ToList();

        info.RemoveAll(o => o.level == Skill_SO.maxLevel - 1);

        return info;
    }
    private bool TryGetSkillData(string key, out SkillContext info)
    {
        if(infos.ContainsKey(key))
        {
            info = this.infos[key];

            return true;
        }
        else
        {
            info = null;

            return false;
        }
    }
}