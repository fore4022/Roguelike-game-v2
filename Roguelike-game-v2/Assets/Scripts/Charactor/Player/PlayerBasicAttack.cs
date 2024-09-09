using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public Coroutine basicAttackCoroutine;

    private Attack_SO basicAttackSO;
    private GameObject prefab;

    private string basicAttackType = "basicAttackSO";
    private float attackSpeed = 1;

    public IEnumerator basicAttacking()
    {
        ObjectPool.CreateScriptableObject(basicAttackType);

        yield return new WaitUntil(() => ObjectPool.scriptableObjects.ContainsKey(basicAttackType));

        basicAttackSO = ObjectPool.GetScriptableObject<Attack_SO>(basicAttackType);

        ObjectPool.CreateToPath(basicAttackSO.attackTypePath);

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

        prefab.GetComponent<BasicAttack>().basicAttackSO = basicAttackSO;
    }
}