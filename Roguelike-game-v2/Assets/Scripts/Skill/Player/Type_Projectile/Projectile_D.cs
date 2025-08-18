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
    [SerializeField, Range(0, 100)]
    private float probability;
    [SerializeField, Range(0.01f,10)]
    private float targetScale;
    [SerializeField, Min(0.01f)]
    private float duration;

    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetRandomDirection();

        transform.SetScale(5, duration);
        
        if(Random.Range(0, 100) <= probability)
        {
            direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        }
        else
        {
            direction = Calculate.GetRandomDirection();
        }

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
            transform.position += direction * (Managers.Game.player.Stat.moveSpeed + so.projectile_Info.speed) * Time.deltaTime;

            yield return null;
        }
    }
}