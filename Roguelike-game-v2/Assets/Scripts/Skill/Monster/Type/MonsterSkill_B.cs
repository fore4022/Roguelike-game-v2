using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkill_B : MonsterSkillBase, IFakeShadowSource
{
    [SerializeField, Min(0.2f)]
    private float duration = 0.75f;

    private readonly Vector3 baseOffset = new(0, 1.5f, 0);
    private const float maxAlpha = 255;
    private const float defaultAlpha = 100;
    private const float preActionDelay = 0.1f;

    private Vector3 targetPosition;
    private Vector3 initialPosition;

    public SpriteRenderer SpriteRender { get { return render; } }
    public Vector3 TargetPosition { get { return targetPosition; } }
    public Vector3 InitialPosition { get { return initialPosition; } }
    public Vector3 CurrentPosition { get { return transform.position; } }
    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        Managers.Game.objectPool.DisableObject(gameObject);
    }
    private void OnDisable()
    {
        if(isInit)
        {
            col.enabled = false;

            SetActive(false);
        }
    }
    private IEnumerator Casting()
    {
        targetPosition = transform.position;
        initialPosition = transform.position += baseOffset;

        SetActive(true);

        transform.SetPosition(targetPosition, duration, Ease.AcceleratedFall);

        StartCoroutine(ColorLerp.ChangeAlpha(render, maxAlpha, defaultAlpha, duration));

        yield return new WaitForSeconds(duration - preActionDelay);

        col.enabled = true;

        yield return new WaitForSeconds(preActionDelay);

        Managers.Game.objectPool.DisableObject(gameObject);
    }
}