using System.Collections;
using UnityEngine;
public class Monster_D : BasicMonster
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCooldown;

    private WaitForSeconds cooldown;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(RepeatBehavior());
    }
    protected override void Init()
    {
        cooldown = new(dashCooldown);

        base.Init();
    }
    private IEnumerator RepeatBehavior()
    {
        canSwitchDirection = true;

        yield return cooldown;

        if(isVisible)
        {
            float totalTime = 0;

            canSwitchDirection = false;

            while(totalTime != dashDuration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > dashDuration)
                {
                    totalTime = dashDuration;
                }

                speedMultiplier = Mathf.Lerp(dashSpeed, speedMultiplierDefault, Ease.InExpo(totalTime / dashDuration));

                yield return null;
            }

            canSwitchDirection = true;
        }

        StartCoroutine(RepeatBehavior());
    }
}