using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_C : MonsterSkill_Base
{
    [SerializeField, Min(1)]
    private float slowDown = 0;
    [SerializeField, Min(0.1f)]
    private float slowDuration;
    [SerializeField]
    private float targetPositionY;
    [SerializeField]
    private float targetScale;

    private const float triggerTime = 0.975f;

    private float defaultPositionY;
    private float defaultRadius;

    protected new CircleCollider2D col { get { return base.col as CircleCollider2D; } }
    protected override void Init()
    {
        base.Init();

        defaultPositionY = col.offset.y;
        defaultRadius = col.radius;
    }
    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.CompareTag("Player"))
        {
            Managers.Game.player.move.SetSlowDown(slowDown, slowDuration);
        }
    }
    private void OnDisable()
    {
        if(isInit)
        {
            col.offset = new(0, defaultPositionY);
            col.radius = defaultRadius;
        }
    }
    private float GetVale()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime / triggerTime;
    }
    private IEnumerator Casting()
    {
        Vector2 lerpedOffset = new();
        float lerpedScale;
        float value;

        SetActive(true);

        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= triggerTime)
        {
            value = GetVale();
            lerpedOffset.y = Mathf.Lerp(targetPositionY, defaultPositionY, value);
            lerpedScale = Mathf.Lerp(targetScale, defaultRadius, value);
            col.offset = lerpedOffset;
            col.radius = lerpedScale;

            yield return null;
        }

        SetActive(false);
    }
}