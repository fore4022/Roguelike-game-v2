using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 투사체 형식의 스킬의 기본 구현이다.
/// </para>
/// ObjectPool로 회하는 것을 구현하였다.
/// </summary>
public class ProjectileSkill : Skill
{
    protected Coroutine moving = null;
    protected Vector3 direction;

    private const float collectDelay = 3;

    private Plane[] planes = new Plane[6];
    private Coroutine collect = null;
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
            if(collect == null)
            {
                collect = StartCoroutine(Collecting());
            }
        }
    }
    private IEnumerator Collecting()
    {
        yield return delay;

        collect = baseCast = null;
    }
}