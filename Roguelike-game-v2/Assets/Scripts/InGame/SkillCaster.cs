using System.Collections;
using UnityEngine;
public class SkillCaster
{
    private Skill_SO so = null;
    private WaitForSeconds coolTime;
    private WaitForSeconds delay;

    private Coroutine cast;
    private string attackType;
    private int level;

    public int Level 
    {
        set
        {
            level = value;

            Set();
        }
    }
    public void SetAttackType(string attackType)
    {
        this.attackType = attackType;

        cast = CoroutineHelper.Start(Casting());
    }
    private void Set()
    {
        coolTime = new(so.coolTime[level]);

        if(so.isMultiCast)
        {
            delay = new(so.multiCast.delay[level]);
        }
    }
    public void CastingStop()
    {
        CoroutineHelper.Stop(cast);
    }
    private IEnumerator Casting()
    {
        so = Managers.Game.so_Manage.GetScriptableObject<Skill_SO>(attackType);

        yield return new WaitUntil(() => so != null);

        Set();

        if(!so.isMultiCast)
        {
            while (true)
            {
                yield return coolTime;

                Managers.Game.objectPool.ActiveObject(attackType);
            }
        }
        else
        {
            int i;

            while(true)
            {
                yield return coolTime;

                for(i = 0; i < so.multiCast.count[level]; i++)
                {
                    Managers.Game.objectPool.ActiveObject(attackType);

                    yield return delay;
                }
            }
        }
    }
}