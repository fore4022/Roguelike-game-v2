using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    private Coroutine basicAttack = null;

    private IEnumerator basicAttacking()
    {
        GameObject attack = AddressableAssetLoad().Result;

        while (true)
        {
            yield return new WaitForSeconds(1);

            Attack();
        }
    }
    private async Task<GameObject> AddressableAssetLoad()
    {
        return await Util.LoadToPath<GameObject>("test");
    }
    private void Attack()
    {
        Debug.Log("Attack");
    }
}