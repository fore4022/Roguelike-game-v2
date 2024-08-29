using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public BasicAttack_SO basicAttackSO;

    private GameObject prefab;

    private float attackSpeed = 1;
    private int createCount = 40;

    public float Damage { get { return Managers.Game.player.Stat.damage * basicAttackSO.damageCoefficient; } }
    public IEnumerator basicAttacking()
    {
        LoadBasicAttackSO(Managers.Game.player.basicAttackTypeName);

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
    public IEnumerator ChangeBasicAttack(string attackType)
    {
        LoadBasicAttackSO(attackType);

        yield return new WaitUntil(() => basicAttackSO);

        yield return null;
    }
    private void Attack()
    {
        Vector3 targetPosition = EnemyDetection.findNearestEnemy().transform.position;
        Vector3 direction = Direction(targetPosition);
        Vector3 localSize = new Vector2(basicAttackSO.attackSize, basicAttackSO.attackSize);
        Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        prefab.transform.position = AttackPoint(targetPosition, direction);
        prefab.transform.rotation = quaternion;
        prefab.transform.localScale = localSize;
    }
    private Vector3 AttackPoint(Vector3 targetPosition, Vector3 direction)
    {
        Vector2 attackPoint = direction * basicAttackSO.attackRange;

        return attackPoint;
    }
    private Vector3 Direction(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - Managers.Game.player.gameObject.transform.position).normalized;

        return direction;
    }
    private async void LoadBasicAttackSO(string attackType)
    {
        basicAttackSO = await Util.LoadToPath<BasicAttack_SO>(attackType);
    }
}