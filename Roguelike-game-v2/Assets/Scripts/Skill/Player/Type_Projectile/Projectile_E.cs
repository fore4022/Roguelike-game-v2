using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 적의 위치를 향해 날아가며, 
/// </para>
/// 
/// </summary>
public class Projectile_E : ProjectileSkill, ISkill
{
    [SerializeField]
    private Collider2D effectCollider;

    [SerializeField]
    private Vector2 castRange;
    [SerializeField, Min(0.01f)]
    private float castDelay;

    private const int initialRotationAngle_Max = 1080;
    private const int initialRotationAngle_Min = 720;

    private Vector3 targetPosition;
    private Vector2 castingPosition;
    private bool isExplosion = false;

    public bool Finished { get { return isExplosion && animator.GetCurrentAnimatorStateInfo(0).IsName(so.projectile_Info.animationName); } }
    public void Set()
    {
        animator.Play("default");

        castingPosition = so.adjustmentPosition + new Vector2(Random.Range(-castRange.x / 2, castRange.x / 2), Random.Range(-castRange.y / 2, castRange.y / 2));
        targetPosition = EnemyDetection.GetRandomVector();
        transform.position = Managers.Game.player.transform.position;
        transform.rotation = Calculate.GetQuaternion(Calculate.GetRandomVector());

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
        effectCollider.enabled = false;
        isExplosion = false;
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

        direction = Calculate.GetDirection(targetPosition + so.adjustmentRotation);

        transform.SetRotation(direction, castDelay);

        yield return new WaitForSeconds(castDelay * 2);

        Vector3 afterPosition = new();
        float sign = Mathf.Sign(direction.x);

        totalTime = 0;
        animator.speed = 1;
        defaultCollider.enabled = true;

        while(transform.position.x != targetPosition.x)
        {
            totalTime += Time.deltaTime;
            afterPosition = transform.position + direction * so.projectile_Info.speed * Time.deltaTime;

            if(afterPosition.x * sign > targetPosition.x)
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
}