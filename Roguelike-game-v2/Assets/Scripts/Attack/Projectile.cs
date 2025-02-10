using System.Collections;
using UnityEngine;
public abstract class Projectile : Attack
{
    protected Coroutine moving = null;
    protected Vector3 direction;
    protected int penetration_count;

    private const float collectDelay = 5;

    private Plane[] planes = new Plane[6];
    private Coroutine collect;

    protected void Update()
    {
        if(attack != null)
        {
            IsInvisible();
        }
    }
    private void IsInvisible()
    {
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

        StopCoroutine(attack);

        attack = null;
    }
    protected abstract override void Enter(GameObject go);
    protected abstract override IEnumerator Attacking();
    protected abstract IEnumerator Moving();
}