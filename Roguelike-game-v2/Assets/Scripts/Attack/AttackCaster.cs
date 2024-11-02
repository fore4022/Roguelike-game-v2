using System.Collections;
using UnityEngine;
public class AttackCaster
{    
    private string attackType;
    private float duration;

    public void SetAttackType(string attackType)
    {
        this.attackType = attackType;

        //duration = 

        Util.GetMonoBehaviour().StartCoroutine(Attacking());
    }
    private void Attack()
    {
        ObjectPool.ActiveObject(attackType);
    }
    public IEnumerator Attacking()
    {
        while (true)
        {       
            Attack();

            yield return new WaitForSeconds(0);//Managers.Game.player.Stat.attackSpeed
        }
    }
}