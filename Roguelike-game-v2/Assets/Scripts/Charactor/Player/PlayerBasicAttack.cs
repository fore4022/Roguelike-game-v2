using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    public float Damage { get { return Managers.Game.player.Stat.damage * basicAttackSO.damageCoefficient; } }

    public BasicAttack_SO basicAttackSO;

    private GameObject prefab;

    private float attackSpeed = 1;

    private IEnumerator WaitForLoad()
    {
        Task<BasicAttack_SO> load = new Task<BasicAttack_SO>(() =>
        {
            return Util.LoadToPath<BasicAttack_SO>(Managers.Game.player.basicAttackTypeName).Result;
        });

        basicAttackSO = load.Start();

        yield return new WaitUntil(() => load.IsCompleted);

    }
    public IEnumerator basicAttacking()
    {
        yield return WaitForLoad();
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
}