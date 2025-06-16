using System.Collections;
using UnityEngine;
public class ProjectileSkill : Skill
{
    protected Coroutine moving = null;
    protected Vector3 direction;

    private const float collectDelay = 3;

    private Plane[] planes = new Plane[6];
    private Coroutine collect;
    private WaitForSeconds delay = new(collectDelay);

    protected void FixedUpdate()
    {
        IsInvisible();
    }
    private void IsInvisible()
    {
        if(baseCast == null)
        {
            return; 
        }

        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, defaultCollider.bounds))
        {
            if(collect != null)
            {
                StopCoroutine(collect);

                collect = null;
            }
        }
        else
        {
            collect = StartCoroutine(Collecting());
        }
    }
    private IEnumerator Collecting()
    {
        yield return delay;

        baseCast = null;
    }
}