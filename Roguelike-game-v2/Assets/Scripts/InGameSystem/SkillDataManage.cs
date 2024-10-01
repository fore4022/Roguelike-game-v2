using System;
using System.Collections.Generic;
public class SkillDataManage
{
    private Dictionary<string, (AttackInformation_SO, Action<int>, int)> attackInformation = null;

    public void SetDictionaryItem(AttackInformation_SO so)
    {
        if(attackInformation.ContainsKey(so.attackName))
        {
            attackInformation.Add(so.attackName, (so, null, 0));
        }
    }
    public void SetValue(string key, int levelDelta = 1)
    {
        if (attackInformation.TryGetValue(key, out (AttackInformation_SO, Action<int>,  int) info))
        {
            info.Item3 += levelDelta;

            info.Item2.Invoke(info.Item3);
        }
    }
    public (AttackInformation_SO, int) GetSkillInfo(string key)
    {


        return;
    }
}