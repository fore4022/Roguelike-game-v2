using System.Collections.Generic;
public class AttackCasterManage
{
    private Dictionary<string, AttackCaster> Casters = new();

    public AttackCaster GetCaster(string type)
    {
        if(Casters.ContainsKey(type))
        {
            return Casters[type];
        }

        return default(AttackCaster);
    }
    public void CreateCaster(string type)
    {
        if(!Casters.ContainsKey(type))
        {
            AttackCaster caster = new();

            caster.SetAttackType(type);

            Casters.Add(type, caster);
        }
    }
    public AttackCaster CreateAndGetCaster(string type)
    {
        if (!Casters.ContainsKey(type))
        {
            AttackCaster caster = new();

            caster.SetAttackType(type);

            Casters.Add(type, caster);

            return Casters[type];
        }

        return default(AttackCaster);
    }
    public void DestroyCaster(string type)
    {
        if(Casters.ContainsKey(type))
        {
            Casters[type] = null;

            Casters.Remove(type);
        }
    }
}