using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkill_B : MonsterSkillBase, IFakeShadowSource
{
    [SerializeField, Min(0.2f)]
    private float duration = 0.5f;
    [SerializeField]
    private Vector3 baseOffset = new(0, 2f, 0);

    private const float preActionDelay = 0.035f;

    private Color defaultColor;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private float scaleValue;

    public SpriteRenderer SpriteRender { get { return render; } }
    public Vector3 TargetPosition { get { return targetPosition; } }
    public Vector3 InitialPosition { get { return initialPosition; } }
    public Vector3 CurrentPosition { get { return transform.position; } }
    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void Init()
    {
        base.Init();

        scaleValue = transform.localScale.x;
        defaultColor = render.color;
        initialScale = new Vector2(scaleValue, scaleValue);
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if(isInit)
        {
            col.enabled = false;

            transform.Kill();
            SetActive(false);
        }
    }
    private IEnumerator Casting()
    {
        targetPosition = transform.position;
        initialPosition = transform.position += baseOffset;
        transform.localScale = initialScale;

        SetActive(true);

        transform.SetScale(scaleValue / 7 * 3, duration)
            .SetPosition(targetPosition, duration, Ease.AcceleratedFall);

        StartCoroutine(ColorLerp.ChangeColor(render, Color.white, defaultColor, duration));

        yield return new WaitForSeconds(duration - preActionDelay * 2);

        col.enabled = true;

        yield return new WaitForSeconds(preActionDelay);

        gameObject.SetActive(false);
    }
}