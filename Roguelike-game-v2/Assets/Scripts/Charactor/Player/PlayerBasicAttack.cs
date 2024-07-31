using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    public IEnumerator basicAttacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            Attack();
        }
    }
    private async Task<GameObject> AddressableAssetLoad()
    {
        return await Util.LoadToPath<GameObject>("");//
    }
    private void Attack()
    {
        Debug.Log("Attack");
    }
}