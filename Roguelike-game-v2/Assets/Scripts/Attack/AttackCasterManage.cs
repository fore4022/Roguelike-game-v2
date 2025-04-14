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

        return default;
    }
    public AttackCaster CreateAndGetCaster(string type)
    {
        if(!Casters.ContainsKey(type))
        {
            UnityEngine.Debug.Log("C");

            AttackCaster caster = new();

            caster.SetAttackType(type);
            Casters.Add(type, caster);

            return Casters[type];
        }

        return default;
    }
    public void UpdateCasterLevel(string type, int level)
    {
        Casters[type].Level = level;
    }
    public void DestroyCaster(string type)
    {
        if(Casters.ContainsKey(type))
        {
            Casters[type] = null;

            Casters.Remove(type);
        }
    }
    public void StopAllCaster()
    {
        foreach(AttackCaster caster in Casters.Values)
        {
            caster.CastingStop();
        }
    }
}