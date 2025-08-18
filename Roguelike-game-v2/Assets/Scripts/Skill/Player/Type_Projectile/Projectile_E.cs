using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 
/// </para>
/// 
/// </summary>
public class Projectile_E : ProjectileSkill, IProjectile
{
    [SerializeField]
    private Collider2D effectCollider;

    [SerializeField]
    private Vector3 castPosition;
    [SerializeField]
    private Vector2 castRange;
    [SerializeField, Min(0.01f)]
    private float castDelay;

    private const int initialRotationAngle_Max = 1080;
    private const int initialRotationAngle_Min = 360;

    private bool isExplosion = false;

    public bool Finished { get { return true; } } //
    public void Set()
    {
        direction = EnemyDetection.GetRandomEnemyPosition();
        transform.position = Managers.Game.player.transform.position;
        transform.rotation = Calculate.GetQuaternion(Calculate.GetRandomVector());

        StartCoroutine(Attacking());
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
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
    private IEnumerator Attacking()
    {
        transform.SetPosition(transform.position + castPosition, castDelay)
            .SetRotation(new(0, 0, Random.Range(initialRotationAngle_Min, initialRotationAngle_Max)), castDelay);
        
        yield return new WaitForSeconds(castDelay);

        transform.SetRotation(direction, castDelay);

        yield return new WaitForSeconds(castDelay / 2);

        animator.speed = 1;
        defaultCollider.enabled = true;

        StartCoroutine(Moving());
    }
}