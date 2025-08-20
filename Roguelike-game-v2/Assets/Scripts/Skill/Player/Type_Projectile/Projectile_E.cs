using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���� �� ������ ���Ÿ� ����
/// </para>
/// ȭ���� ������ ��ġ�� ���� ���ư���. ���� �о�� �ʴ´�.
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
    private const int animation_Angle = 30;

    private Vector3 targetPosition;
    private Vector2 castingPosition;
    private float sign_Angle;
    private bool isExplosion = false;

    public bool Finished { get { return isExplosion && animator.GetCurrentAnimatorStateInfo(0).IsName(so.projectile_Info.animationName); } }
    public void Set()
    {
        animator.Play("default");

        targetPosition = EnemyDetection.GetNearestEnemyPosition();
        castingPosition = so.adjustmentPosition + new Vector2(Random.Range(-castRange.x / 2, castRange.x / 2), Random.Range(-castRange.y / 2, castRange.y / 2));
        transform.position = Managers.Game.player.transform.position;
        transform.rotation = Calculate.GetQuaternion(Calculate.GetDirection(targetPosition));
        direction = Calculate.GetDirection(targetPosition, (Vector2)Managers.Game.player.transform.position + castingPosition);

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
    private IEnumerator Attacking()
    {
        float totalTime = 0;
        
        transform.SetRotation(new(0, 0, Random.Range(initialRotationAngle_Min, initialRotationAngle_Max)), castDelay * 2, EaseType.OutCubic);

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

        yield return new WaitForSeconds(castDelay);

        sign_Angle = Random.Range(0, 2) == 1 ? -1 : 1;

        transform.SetRotation(Calculate.GetQuaternion(direction).eulerAngles + new Vector3(0, 0, (360 + animation_Angle * sign_Angle) - transform.rotation.eulerAngles.z % 360), castDelay, EaseType.OutCirc)
            .SetRotation(new(0, 0, animation_Angle * -sign_Angle), castDelay * 2, EaseType.OutCubic, TweenOperation.Append);

        yield return new WaitForSeconds(castDelay * 3);

        Vector3 remainingDistance = targetPosition - (transform.position + direction * so.projectile_Info.speed * Time.deltaTime);
        Vector3 afterPosition = new();

        totalTime = 0;
        animator.speed = 1;
        defaultCollider.enabled = true;

        while(true)
        {
            totalTime += Time.deltaTime;
            afterPosition = transform.position + direction * so.projectile_Info.speed * Time.deltaTime;

            if(remainingDistance.sqrMagnitude >= (targetPosition - afterPosition).sqrMagnitude)
            {
                remainingDistance = targetPosition - afterPosition;
                transform.position = afterPosition;
            }
            else
            {
                transform.position = targetPosition;

                break;
            }

            yield return null;
        }

        animator.Play(so.projectile_Info.animationName);

        isExplosion = true;
    }
}