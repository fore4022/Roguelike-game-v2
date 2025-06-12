using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���Ÿ� ���� ���� : ���� ũ�Ⱑ Ŀ����.
/// </para>
/// ������ ������ ���ؼ� ���ư���.
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