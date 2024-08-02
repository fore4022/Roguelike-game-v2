using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    [SerializeField]
    private string prefabName = "";

    private GameObject prefab;

    public IEnumerator basicAttacking()
    {
        prefab = AddressableAssetLoad(prefabName).Result;

        while (true)
        {
            yield return new WaitForSeconds(1);

            Attack();
        }
    }
    private async Task<GameObject> AddressableAssetLoad(string prefabName)
    {
        return await Util.LoadToPath<GameObject>(prefabName);
    }
    private void Attack()
    {
        
    }
}