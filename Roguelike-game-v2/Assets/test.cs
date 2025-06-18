using UnityEngine;
public class test : MonoBehaviour, IFakeShadowSource
{
    public SpriteRenderer SpriteRender => render;

    public SpriteRenderer render;

    public Vector3 TargetPosition => new(0, -4);

    public Vector3 InitialPosition => new(0, 0);

    public Vector3 CurrentPosition => transform.position;

    private void Start()
    {
        transform.SetPosition(new(0, -4), 4, Ease.AcceleratedFall);
    }
}