using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    private GameObject prefab = null;

    public IEnumerator basicAttacking()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);//Managers.Game.player.Stat.attackSpeed

            Attack();
        }
    }
    private void Attack()
    {
        GameObject basicAttack = ObjectPool.GetOrActiveObject(Managers.Game.player.basicAttackPrefabName);
        
        Vector3 targetPosition = EnemyDetection.findNearestEnemy().transform.position;
        Vector3 direction = Direction(targetPosition);

        basicAttack.transform.position = AttackPoint(targetPosition, direction);
        basicAttack.transform.rotation = Quaternion.Euler(direction);
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