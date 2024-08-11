using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public float Damage { get { return Managers.Game.player.Stat.damage * basicAttackSO.damageCoefficient; } }

    private BasicAttack_SO basicAttackSO = null;

    private GameObject prefab = null;

    private float attackSpeed;

    public IEnumerator basicAttacking()
    {
        while (true)
        {
            prefab = ObjectPool.GetOrActiveObject(Managers.Game.player.basicAttackPrefabName);

            if (prefab == null)
            {
                yield return null;
            }
            else
            {
                Attack();

                yield return new WaitForSeconds(1);//Managers.Game.player.Stat.attackSpeed
            }
        }
    }
    private void Attack()
    {
        Vector3 targetPosition = EnemyDetection.findNearestEnemy().transform.position;
        Vector3 direction = Direction(targetPosition);
        Quaternion quaternion = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        prefab.transform.position = AttackPoint(targetPosition, direction);
        prefab.transform.rotation = quaternion;
    }
    private Vector3 AttackPoint(Vector3 targetPosition, Vector3 direction)
    {
        Vector2 attackPoint = targetPosition + direction * 2;

        return attackPoint;
    }
    private Vector3 Direction(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - Managers.Game.player.gameObject.transform.position).normalized;

        return direction;
    }
}