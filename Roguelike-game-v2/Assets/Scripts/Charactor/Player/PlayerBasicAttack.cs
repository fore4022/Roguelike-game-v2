using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public float Damage { get { return Managers.Game.player.Stat.damage * basicAttackSO.damageCoefficient; } }

    public BasicAttack_SO basicAttackSO;

    private GameObject prefab;

    private float attackSpeed = 1;

    public IEnumerator basicAttacking()
    {
        Util.GetScriptableObject<BasicAttack_SO>(Managers.Game.player.basicAttackTypeName);

        while (true)
        {
            if (Util.scriptableObject != null)
            {
                basicAttackSO = (BasicAttack_SO)Util.scriptableObject;

                break;
            }

            yield return null;
        }

        while (true)
        {
            prefab = ObjectPool.GetOrActiveObject(basicAttackSO.attackType.name);

            if (prefab != null)
            {
                Attack();

                yield return new WaitForSeconds(attackSpeed);
            }
            else
            {
                yield return null;
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