using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class FakeShadowRenderer : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float shadowOffsetY = 0;
    [SerializeField, Range(1.1f, 2f)]
    private float shadowScale = 1.1f;

    private readonly float alphaRange = 255 - alphaMin;
    private const float alphaMin = 100;
    private const float _start = 0;
    private const float _end = 1;

    private IFakeShadowSource source = null;
    private SpriteRenderer render;

    private Color alphaColor;
    private Vector3 shadowOffset;
    private Vector3 vec;
    private float value;
    private float scale;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        render.sprite = source.SpriteRender.sprite;

        Factor();
        AdjustmentPosition();
        AdjustmentScale();
        AdjustmentAlpha();
    }
    private void Init()
    {
        transform.parent.TryGetComponent(out source);
        shadowOffset = new(0, shadowOffsetY);

        if(source == null)
        {
            gameObject.SetActive(false);
        }
    }
    private void AdjustmentPosition()
    {
        if(source.motionType == ShadowMotionType.AcceleratedFall)
        {
            vec = source.CurrentPosition - shadowOffset * AcceleratedFall();
        }
        else
        {
            vec = source.CurrentPosition - shadowOffset * Parabola();
        }

        transform.position =  vec;
    }
    private void AdjustmentScale()
    {
        if(source.motionType == ShadowMotionType.AcceleratedFall)
        {
            scale = shadowScale - shadowScale * AcceleratedFall();
        }
        else
        {
            scale = shadowScale - shadowScale * Parabola();
        }

        transform.localScale = Calculate.GetVector(scale);
    }
    private void AdjustmentAlpha()
    {
        alphaColor = render.color;

        if(source.motionType == ShadowMotionType.AcceleratedFall)
        {
            alphaColor.a = alphaMin + alphaRange * AcceleratedFall();
        }
        else
        {
            alphaColor.a = alphaMin + alphaRange * Parabola();
        }

        render.color = alphaColor;
    }
    private void Factor()
    {
        value = Mathf.Lerp(source.TargetPosition.y, source.InitialPosition.y, source.CurrentPosition.y);
    }
    private float AcceleratedFall()
    {
        return Mathf.Lerp(_start, _end, value * value);
    }
    private float Parabola()
    {
        return _start + 4 * value * (1 - value);
    }
}