using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 원거리 범위 공격 : 점점 크기가 커진다.
/// </para>
/// 무작위 방향을 향해서 날아간다.
/// </summary>
public class Projectile_D : ProjectileSkill, IProjectile
{
    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetRandomDirection();
        transform.SetScale(5, 12);
        moving = StartCoroutine(Moving());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private void OnDisable()
    {
        if(isInit)
        {
            defaultCollider.enabled = false;

            transform.Kill().SetScale(1);
        }
    }
    public IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}