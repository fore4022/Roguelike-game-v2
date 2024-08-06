using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    [SerializeField]
    private string prefabName = "basicAttack";

    private GameObject prefab = null;

    private void basicAttackStart()
    {
        Task.Run(async () => prefab = await AddressableAssetLoad(prefabName));
    }
    public IEnumerator basicAttacking()
    {
        basicAttackStart();

        while (true)
        {
            if(prefab == null)
            {
                Debug.Log(prefab);
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
    private async Task<GameObject> AddressableAssetLoad(string prefabName)
    {
        Debug.Log("test");
        return await Util.LoadToPath<GameObject>(prefabName);
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