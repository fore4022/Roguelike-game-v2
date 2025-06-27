using System.Collections;
using UnityEngine;
public class Monster_E : BasicMonster
{
    [SerializeField]
    private float rushSpeed = 3;
    [SerializeField]
    private float rushCastingTime = 0.75f;

    protected override void OnEnable()
    {
        base.OnEnable();

        speedMultiplier = speedMultiplierDefault;
        canSwitchDirection = true;

        StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitUntil(() => isVisible);

        yield return new WaitUntil(() => (Managers.Game.player.transform.position - transform.position).magnitude <= Util.CameraWidth / 2);

        float totalTime = 0;

        while(totalTime != rushCastingTime)
        {
            totalTime += Time.deltaTime;

            if(totalTime > rushCastingTime)
            {
                totalTime = rushCastingTime;
            }

            speedMultiplier = Mathf.Lerp(0, speedMultiplierDefault, totalTime / rushCastingTime);

            yield return null;
        }

        speedMultiplier = rushSpeed;
        canSwitchDirection = false;
    }
}