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
        
    }
}/*
    NullReferenceException: Object reference not set to an instance of an object
    Player.Awake () (at Assets/Scripts/Charactor/Player/Player.cs:18)
  */