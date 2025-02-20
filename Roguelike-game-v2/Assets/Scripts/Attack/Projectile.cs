using System.Collections;
using UnityEngine;
public abstract class Projectile : Attack
{
    protected Coroutine moving = null;
    protected Vector3 direction;

    private const float collectDelay = 3;

    private Plane[] planes = new Plane[6];
    private Coroutine collect;
    private WaitForSeconds delay = new(collectDelay);

    protected override void SetAttack()
    {
        moving = StartCoroutine(Moving());
    }
    protected void Update()
    {
        IsInvisible();
    }
    private void IsInvisible()
    {
        if(attack == null)
        {
            return; 
        }

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
        yield return delay;

        attack = null;
    }
    protected override IEnumerator Attacking()
    {
        yield return new WaitUntil(() => moving == null);

        yield return base.Attacking();
    }
    protected abstract override void Enter(GameObject go);
    protected abstract IEnumerator Moving();
}