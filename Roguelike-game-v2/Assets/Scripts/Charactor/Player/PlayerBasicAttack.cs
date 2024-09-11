using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public Coroutine basicAttackCoroutine;

    private Attack_SO basicAttackSO;
    private GameObject prefab;

    private string basicAttackType = "BasicAttack";
    private float attackSpeed = 1;

    private void Attack()
    {
        prefab = ObjectPool.GetOrActiveObject(basicAttackSO.attackType.name);

        prefab.GetComponent<BasicAttack>().basicAttackSO = basicAttackSO;
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
        basicAttackSO = ObjectPool.GetScriptableObject<Attack_SO>(basicAttackType);

        while (true)
        {
            Attack();

            yield return new WaitForSeconds(attackSpeed);
        }
    }
}