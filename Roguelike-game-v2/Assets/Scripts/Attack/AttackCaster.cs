using System.Collections;
using UnityEngine;
public class AttackCaster
{
    private Attack_SO attackSO = null;

    private string attackType;
    private int level;

    public int Level 
    {
        set
        {
            level = value;
        }
    }
    public void SetAttackType(string attackType)
    {
        this.attackType = attackType;

        Util.GetMonoBehaviour().StartCoroutine(Attacking());
    }
    public IEnumerator Attacking()
    {
        attackSO = Managers.Game.inGameData.dataInit.objectPool.GetScriptableObject<Attack_SO>(attackType);

        yield return new WaitUntil(() => attackSO != null);

        while (true)
        {
            yield return new WaitForSeconds(attackSO.coolTime[level]);
            
            Managers.Game.inGameData.dataInit.objectPool.ActiveObject(attackType);
        }
    }
}