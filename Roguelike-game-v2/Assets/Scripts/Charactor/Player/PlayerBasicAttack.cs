using System.Collections;
using UnityEngine;
public class PlayerBasicAttack : MonoBehaviour
{
    private Coroutine basicAttack = null;

    private void Start()
    {
        basicAttack = StartCoroutine(basicAttacking());
    }
    private IEnumerator basicAttacking()
    {
        while(true)
        {
            Attack();

            yield return new WaitForSeconds(Managers.Game.player.Stat.attackSpeed);
        }
    }
    private void Attack()
    {
        //Instantiate();
    }
}