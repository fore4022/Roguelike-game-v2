using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public Coroutine basicAttackCoroutine;

    private string basicAttackType = "BasicAttack";

    private float attackSpeed = 1;

    private void Attack()
    {
        GameObject prefab = ObjectPool.GetOrActiveObject(basicAttackType);
    }
    public void ChangeBasicAttack(string attackType)
    {
        MonoBehaviour mono = Util.GetMonoBehaviour();

        basicAttackType = attackType;

        if (basicAttackCoroutine != null)
        {
            mono.StopCoroutine(basicAttackCoroutine);
        }

        mono.StartCoroutine(basicAttacking());
    }
    public IEnumerator basicAttacking()
    {
        while (true)
        {
            Attack();

            yield return new WaitForSeconds(attackSpeed);
        }
    }
}