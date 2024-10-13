using System.Collections;
using UnityEngine;
public class AttackCaster
{
    public Coroutine basicAttackCoroutine;

    private string basicAttackType = "BasicAttack";//

    private int level;

    public int Level { get { return level; } }
    private void StartAttack()
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
            StartAttack();

            yield return new WaitForSeconds(Managers.Game.player.Stat.attackSpeed);
        }
    }
}