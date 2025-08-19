using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 관통 및 폭발형 원거리 공격
/// </para>
/// 화면의 무작위 위치를 향해 날아간다. 적을 밀어내지 않는다.
/// </summary>
public class Projectile_E : ProjectileSkill, IProjectile
{
    [SerializeField]
    private Collider2D effectCollider;

    [SerializeField]
    private Vector2 castRange;
    [SerializeField, Range(0, 100)]
    private float probability;
    [SerializeField, Min(0.01f)]
    private float castDelay;

    private const int initialRotationAngle_Max = 1080;
    private const int initialRotationAngle_Min = 720;
    private const int animation_Angle = 30;

    private Vector3 targetPosition;
    private Vector2 castingPosition;
    private float sign_Angle;
    private bool isExplosion = false;

    public bool Finished { get { return isExplosion && animator.GetCurrentAnimatorStateInfo(0).IsName(so.projectile_Info.animationName); } }
    public void Set()
    {
        animator.Play("default");

        if(Random.Range(0, 100) <= probability)
        {
            targetPosition = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        }
        else
        {
            targetPosition = (Vector2)Managers.Game.player.transform.position + EnemyDetection.GetRandomVector();
        }

        castingPosition = so.adjustmentPosition + new Vector2(Random.Range(-castRange.x / 2, castRange.x / 2), Random.Range(-castRange.y / 2, castRange.y / 2));
        transform.position = Managers.Game.player.transform.position;
        transform.rotation = Calculate.GetQuaternion(targetPosition);

        StartCoroutine(Attacking());
    }
    public void SetCollider()
    {
        if(isExplosion)
        {
            effectCollider.enabled = false;
            defaultCollider.enabled = false;
        }
        else
        {
            effectCollider.enabled = true;
            defaultCollider.enabled = false;
        }
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
        transform.rotation = Quaternion.identity;
        effectCollider.enabled = false;
        isExplosion = false;
    }
    public IEnumerator Moving()
    {
        float totalTime = 0;

        while(totalTime != castDelay * 5)
        {
            totalTime += Time.deltaTime;

            if(totalTime > castDelay * 5)
            {
                totalTime = castDelay * 5;
            }

            transform.position = (Vector2)Managers.Game.player.transform.position + castingPosition;

            yield return null;
        }

        moving = null;
    }
    private IEnumerator Attacking()
    {
        float totalTime = 0;
        
        transform.SetRotation(new(0, 0, Random.Range(initialRotationAngle_Min, initialRotationAngle_Max)), castDelay, EaseType.OutCubic);

        while(totalTime != castDelay)
        {
            totalTime += Time.deltaTime;

            if(totalTime > castDelay)
            {
                totalTime = castDelay;
            }

            transform.position = Managers.Game.player.transform.position + Vector3.Lerp(new(), castingPosition, totalTime / castDelay);

            yield return null;
        }

        direction = Calculate.GetDirection(targetPosition, (Vector2)Managers.Game.player.transform.position + castingPosition);

        transform.SetRotation(Calculate.GetQuaternion(direction).eulerAngles + new Vector3(0, 0, (360 + animation_Angle * sign_Angle) - transform.rotation.eulerAngles.z % 360), castDelay, EaseType.OutCirc)
            .SetRotation(new(0, 0, animation_Angle), castDelay * 5, TweenOperation.Append);

        moving = StartCoroutine(Moving());

        yield return new WaitForEndOfFrame();

        yield return new WaitUntil(() => moving == null);

        transform.SkipToEnd();

        Vector3 afterPosition = new();

        totalTime = 0;
        animator.speed = 1;
        defaultCollider.enabled = true;

        while(transform.position.x != targetPosition.x)
        {
            totalTime += Time.deltaTime;
            afterPosition = transform.position + direction * so.projectile_Info.speed * Time.deltaTime;

            if(afterPosition.magnitude > targetPosition.magnitude)
            {
                transform.position = targetPosition;
            }
            else
            {
                transform.position = afterPosition;
            }

            yield return null;
        }

        animator.Play(so.projectile_Info.animationName);

        isExplosion = true;
    }
} // (Vector2)Managers.Game.player.transform.position + 