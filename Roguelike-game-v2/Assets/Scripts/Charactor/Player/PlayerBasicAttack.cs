using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    [SerializeField]
    private string prefabName = "basicAttack";

    private GameObject prefab = null;

    public IEnumerator basicAttacking()
    {
        Task prefabLoad = Task.Run(async() => prefab = await Util.LoadToPath<GameObject>(prefabName));

        yield return new WaitUntil(() => prefabLoad.IsCompleted);

        while (true)
        {
            if (prefab == null)
            {
                Debug.Log(prefabLoad.Status);
                yield return null;
            }
            else
            {
                Debug.Log(prefab);
                yield return new WaitForSeconds(1);//Managers.Game.player.Stat.attackSpeed

                Attack();
            }
        }
    }
    private void Attack()
    {
        Vector3 targetPosition = EnemyDetection.findNearestEnemy().transform.position;
        Vector3 direction = Direction(targetPosition);

        GameObject.Instantiate(prefab, AttackPoint(targetPosition, direction), Quaternion.Euler(direction));
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