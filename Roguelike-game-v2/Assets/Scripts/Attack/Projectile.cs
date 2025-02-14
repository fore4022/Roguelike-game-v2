using System.Collections;
using UnityEngine;
public abstract class Projectile : Attack
{
    protected Coroutine moving = null;
    protected Vector3 direction;
    protected int penetration_count;

    private const float collectDelay = 3;

    private Plane[] planes = new Plane[6];
    private Coroutine collect;

    protected void Update()
    {
        IsInvisible();
    }
    private void IsInvisible()
    {
        if(attack == null) { return; }

        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(!GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            collect = StartCoroutine(Collectting());
        }
        else if(collect != null)
        {
            StopCoroutine(collect);

            collect = null;
        }
    }
    private IEnumerator Collectting()
    {
        yield return new WaitForSeconds(collectDelay);

        attack = null;
    }
    protected override IEnumerator Attacking()
    {
        yield return base.Attacking();
    }
    protected abstract override void Enter(GameObject go);
    protected abstract IEnumerator Moving();
}