using System.Collections;
using UnityEngine;
public class AttackCaster
{
    private Attack_SO so = null;
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

        cast = Util.GetMonoBehaviour().StartCoroutine(Casting());
    }
    private void Set()
    {
        coolTime = new(so.coolTime[level]);

        if (so.isMultiCast)
        {
            delay = new(so.multiCast.delay[level]);
        }
    }
    public void CastingStop()
    {
        Util.GetMonoBehaviour().StopCoroutine(cast);
    }
    private IEnumerator Casting()
    {
        so = Managers.Game.inGameData.init.objectPool.GetScriptableObject<Attack_SO>(attackType);

        yield return new WaitUntil(() => so != null);

        Set();

        if(!so.isMultiCast)
        {
            while (true)
            {
                yield return coolTime;

                Managers.Game.inGameData.init.objectPool.ActiveObject(attackType);
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
                    Managers.Game.inGameData.init.objectPool.ActiveObject(attackType);

                    yield return delay;
                }
            }
        }
    }
}