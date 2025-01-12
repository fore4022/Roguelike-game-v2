using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class RenderableObject : MonoBehaviour
{
    protected SpriteRenderer render;
    protected Collider2D col;

    protected bool visible = false;

    private Plane[] planes = new Plane[6];

    protected virtual void Awake()
    {
        render = GetComponent<SpriteRenderer>();

        if(TryGetComponent(out Collider2D col))
        {
            this.col = col;
        }
        else
        {
            gameObject.AddComponent<CircleCollider2D>();
        }
    }
    protected virtual void FixedUpdate()
    {
        isInvisible();
    }
    private void isInvisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            visible = true;
        }
        else
        {
            visible = false;
        }
    }
}