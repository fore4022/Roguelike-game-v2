using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class FakeShadowRenderer : MonoBehaviour
{
    private const float alphaMin = 155;
    private readonly float alphaRange = 255 - alphaMin;

    private IFakeShadowSource source = null;
    private SpriteRenderer render;

    private Color alphaColor;
    private Vector3 shadowOffset;
    private float shadowOffsetY = 1.4f;

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

        AdjustmentPosition();
        AdjustmentScale();
        AdjustmentAlpha();
    }
    private void Init()
    {
        shadowOffset = new(0, shadowOffsetY);

        if(transform.parent != null)
        {
            transform.parent.TryGetComponent(out source);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void AdjustmentPosition()
    {
        transform.position = source.CurrentPosition - shadowOffset * (1 - Factor());
    }
    private void AdjustmentScale()
    {
        transform.localScale = Calculate.GetVector(0.65f + Factor() / 2);
    }
    private void AdjustmentAlpha()
    {
        alphaColor = render.color;
        alphaColor.a = ((alphaMin + alphaRange * Factor()) / 255);
        render.color = alphaColor;
    }
    private float Factor()
    {
        return Mathf.Lerp(0, 1, source.CurrentPosition.y / source.TargetPosition.y);
    }
}