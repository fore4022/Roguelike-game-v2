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

    private float defaultPositionY;
    private float defaultScale;

    protected new CircleCollider2D col { get { return base.col as CircleCollider2D; } }
    protected override void Init()
    {
        base.Init();

        defaultPositionY = col.offset.y;
        defaultScale = col.radius;
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
    private IEnumerator Casting()
    {
        SetActive(true);

        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.975f)
        {
            

            yield return null;
        }

        SetActive(false);
    }
}