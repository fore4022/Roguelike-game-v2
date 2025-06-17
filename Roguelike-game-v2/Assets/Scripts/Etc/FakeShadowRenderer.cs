using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class FakeShadowRenderer : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float shadowOffset = 0;

    private IFakeShadowSource source = null;
    private SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        transform.parent.TryGetComponent(out source);

        if(source == null)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        render.sprite = source.CurrentSprite;

        AdjustmentPosition();
        AdjustmentScale();
        AdjustmentAlpha();
    }
    private void AdjustmentPosition()
    {
        if(source.motionType == ShadowMotionType.Fall)
        {

        }
        else
        {

        }

        //transform.position
    }
    private void AdjustmentScale()
    {
        if(source.motionType == ShadowMotionType.Fall)
        {

        }
        else
        {

        }

        //transform.localScale
    }
    private void AdjustmentAlpha()
    {
        if(source.motionType == ShadowMotionType.Fall)
        {

        }
        else
        {

        }

        //render.color.a
    }
}