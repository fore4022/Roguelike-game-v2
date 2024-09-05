using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public Coroutine basicAttackCoroutine;

    private Attack_SO basicAttackSO;
    private GameObject prefab;

    private string basicAttackType = "basicAttackSO";
    private float attackSpeed = 1;
    private int createCount = 40;

    public IEnumerator basicAttacking()
    {
        LoadBasicAttack();

        yield return new WaitUntil(() => basicAttackSO != null);

        ObjectPool.CreateToPath(basicAttackSO.attackTypePath, createCount);

        yield return new WaitUntil(() => ObjectPool.FindObject(basicAttackSO.attackType.name) != null);

        while (true)
        {
            Attack();

            yield return new WaitForSeconds(attackSpeed);
        }
    }
    public void ChangeBasicAttack(string attackType)
    {
        MonoBehaviour mono = Util.GetMonoBehaviour();

        basicAttackType = attackType;

        if(basicAttackCoroutine != null)
        {
            mono.StopCoroutine(basicAttackCoroutine);
        }

        mono.StartCoroutine(basicAttacking());
    }
    private void Attack()
    {
        prefab = ObjectPool.GetOrActiveObject(basicAttackSO.attackType.name);
    }
    private async void LoadBasicAttack()
    {
        basicAttackSO = await Util.LoadToPath<Attack_SO>(basicAttackType);
    }
}