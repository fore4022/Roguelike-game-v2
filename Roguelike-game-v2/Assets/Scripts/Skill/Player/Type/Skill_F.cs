using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 지속 시간 동안 유지되며, 방향을 바꿔 가면서 공격
/// </summary>
public class Skill_F : PlayerSkill, ISkill
{
    [SerializeField]
    private float speed;

    private Coroutine colorVairation = null;
    private Vector3 direction;
    private float currentSpeed;
    private float totalTime = 0;
    private float targetTime = 0;

    public bool Finished { get { return so.duration <= totalTime; } }
    public void Set()
    {
        currentSpeed = speed;
        totalTime = 0;
        targetTime = Mathf.Lerp(totalTime, so.duration, Random.Range(1, so.duration) / so.duration);
        transform.position = Managers.Game.player.gameObject.transform.position + (Vector3)Calculate.GetRandomVector();
        direction = Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition(), transform.position);

        StartCoroutine(Attacking());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private IEnumerator Attacking()
    {
        while(totalTime < so.duration)
        {
            if(totalTime >= targetTime)
            {
                if(totalTime < so.duration - 1)
                {
                    targetTime = Mathf.Lerp(totalTime, so.duration, Random.Range(1, so.duration) / so.duration);
                    direction = Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition(), transform.position);
                }
            }

            transform.position += direction * currentSpeed * Time.deltaTime;
            totalTime += Time.deltaTime;
            
            yield return null;

            if(totalTime > so.duration - 1)
            {
                currentSpeed -= Time.deltaTime;

                if(colorVairation == null)
                {
                    colorVairation = StartCoroutine(ColorUtil.ChangeAlpha(render, 0, render.color.a, 1));
                }
            }
            else
            {
                currentSpeed = Calculate.GetParabolicY(so.duration, speed, totalTime) + 1;
            }
        }

        StopCoroutine(colorVairation);

        colorVairation = null;
        render.color = Color.white;
    }
}