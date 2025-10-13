using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���Ÿ� ���� / ���� Ÿ�� ���Ÿ� ����
/// </para>
/// ���� ����� ���� ���ؼ� ���ư���.
/// </summary>
public class Projectile_A : ProjectileSkill, IProjectile
{
    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition());
        transform.rotation = Calculate.GetQuaternion(direction - so.adjustmentRotation);
        moving = StartCoroutine(Moving());
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
}