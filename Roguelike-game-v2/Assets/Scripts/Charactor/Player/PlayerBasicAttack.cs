using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public Coroutine basicAttackCoroutine;

    private BasicAttack_SO basicAttackSO;
    private GameObject prefab;

    private float attackSpeed = 1;
    private int createCount = 40;

    public IEnumerator basicAttacking()
    {
        LoadBasicAttackSO();

        yield return new WaitUntil(() => basicAttackSO != null);

        ObjectPool.CreateToPath(basicAttackSO.attackTypePath, createCount);

        yield return new WaitUntil(() => ObjectPool.GetObject(basicAttackSO.attackType.name) != null);

        while (true)
        {
            prefab = ObjectPool.GetOrActiveObject(basicAttackSO.attackType.name);

            Attack();

            yield return new WaitForSeconds(attackSpeed);
        }
    }
    public void ChangeBasicAttack(string attackType)
    {
        MonoBehaviour mono = Util.GetMonoBehaviour();

        if(basicAttackCoroutine != null)
        {
            mono.StopCoroutine(basicAttackCoroutine);
        }

        mono.StartCoroutine(basicAttacking());
    }
    private void Attack()
    {
        Vector3 targetPosition = EnemyDetection.findNearestEnemy().transform.position;
        Vector3 direction = Calculate.GetDirection(targetPosition);
        Vector3 localSize = new Vector2(basicAttackSO.attackSize, basicAttackSO.attackSize);
        Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        prefab.transform.position = AttackPoint(targetPosition, direction);
        prefab.transform.rotation = quaternion;
        prefab.transform.localScale = localSize;

        prefab.GetComponent<BasicAttack>().basicAttackSO = basicAttackSO;
    }
    private Vector3 AttackPoint(Vector3 targetPosition, Vector3 direction)
    {
        Vector2 attackPoint = direction * basicAttackSO.attackRange;

        return attackPoint;
    }
    private async void LoadBasicAttackSO()
    {
        basicAttackSO = await Util.LoadToPath<BasicAttack_SO>(Managers.Game.player.basicAttackTypeName);
    }
}