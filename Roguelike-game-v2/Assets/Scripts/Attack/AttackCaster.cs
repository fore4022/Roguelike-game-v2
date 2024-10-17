using System.Collections;
using UnityEngine;
public class AttackCaster
{
    public Coroutine attackCoroutine;

    private string attackType;

    public void SetAttackType(string attackType)
    {
        this.attackType = attackType;

        Util.GetMonoBehaviour().StartCoroutine(Attacking());
    }
    private void StartAttack()
    {
        ObjectPool.GetOrActiveObject(attackType);
    }
    public IEnumerator Attacking()
    {
        while (true)
        {
            StartAttack();

            yield return new WaitForSeconds(Managers.Game.player.Stat.attackSpeed);
        }
    }
}