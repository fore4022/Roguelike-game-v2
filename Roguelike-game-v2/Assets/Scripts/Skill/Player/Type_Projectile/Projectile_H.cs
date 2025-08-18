using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 
/// </para>
/// 
/// </summary>
public class Projectile_H : ProjectileSkill, IProjectile
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = Managers.Game.player.transform.position; // + additional
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    public IEnumerator Moving()
    {
        while (true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
    private IEnumerator Attacking()
    {
        //while()

        yield return null;
    }
}