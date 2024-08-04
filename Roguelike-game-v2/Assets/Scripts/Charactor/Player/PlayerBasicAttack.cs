using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerBasicAttack
{
    [SerializeField]
    private string prefabName = "";

    private GameObject prefab;

    private async void Start()//1
    {
        prefab = AddressableAssetLoad(prefabName).Result;
    }
    public void basicAttackStart()//3
    {
        
    }
    private IEnumerator basicAttacking()//4
    {
        while (true)
        {
            yield return new WaitForSeconds(1);//Managers.Game.player.Stat.attackSpeed

            Attack();
        }
    }
    private async Task<GameObject> AddressableAssetLoad(string prefabName)//2
    {
        return await Util.LoadToPath<GameObject>(prefabName);
    }
    private void Attack()//5
    {
        
    }
}