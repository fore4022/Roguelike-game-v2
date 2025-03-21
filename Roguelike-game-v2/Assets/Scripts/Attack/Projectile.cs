using System.Collections;
using UnityEngine;
public class Projectile : Attack
{
    protected Coroutine moving = null;
    protected Vector3 direction;

    private const float collectDelay = 3;

    private Plane[] planes = new Plane[6];
    private Coroutine collect;
    private WaitForSeconds delay = new(collectDelay);

    protected void Update()
    {
        IsInvisible();
    }
    private void IsInvisible()
    {
        if(baseAttack == null)
        {
            return; 
        }

        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(!GeometryUtility.TestPlanesAABB(planes, defaultCollider.bounds))
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

        baseAttack = null;
    }
}