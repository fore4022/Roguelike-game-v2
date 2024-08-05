using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    [SerializeField]
    private string prefabName = "basicAttack";

    private GameObject prefab;

    private async void Start()
    {
        prefab = await AddressableAssetLoad(prefabName);
    }
    public IEnumerator basicAttacking()
    {
        while (true)
        {
            if(prefab == null)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);//Managers.Game.player.Stat.attackSpeed

            Attack();
        }
    }
    private async Task<GameObject> AddressableAssetLoad(string prefabName)
    {
        return await Util.LoadToPath<GameObject>(prefabName);
    }
    private void Attack()
    {
        Debug.Log(EnemyDetection.findNearestEnemy());
        Debug.Log("attack");
    }
}