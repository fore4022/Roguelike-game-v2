using System.Collections;
using UnityEngine;
public class Attack_F : Attack
{
    [SerializeField]
    private float speed;

    private Coroutine colorVairation = null;
    private Vector3 direction;
    private float currentSpeed;
    private float totalTime = 0;
    private float targetTime = 0;

    protected override void SetAttack()
    {
        currentSpeed = speed;
        totalTime = 0;
        targetTime = Mathf.Lerp(totalTime, so.duration, Random.Range(1, so.duration) / so.duration);
        direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetRandomVector());
        transform.position = Managers.Game.enemyDetection.GetRandomVector();
    }
    protected override IEnumerator Attacking()
    {
        while(totalTime < so.duration)
        {
            if(totalTime >= targetTime)
            {
                if(totalTime < so.duration - 1)
                {
                    targetTime = Mathf.Lerp(totalTime, so.duration, Random.Range(1, so.duration) / so.duration);
                    direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetRandomVector());
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
                    colorVairation = StartCoroutine(ColorLerp.ChangeAlpha(render, 0, render.color.a, 1));
                }
            }
            else
            {
                currentSpeed = Managers.Game.calculate.GetParabolicY(so.duration, speed, totalTime) + 1;
            }
        }

        StopCoroutine(colorVairation);

        colorVairation = null;
        render.color = Color.white;

        yield return base.Attacking();
    }
}