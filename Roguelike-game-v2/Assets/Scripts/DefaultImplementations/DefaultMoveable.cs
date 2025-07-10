using System.Collections;
using UnityEngine;
public class DefaultMoveable : IMoveable, IDefaultImplementable
{
    private MonoBehaviour mono;

    private float slowDown = 0;

    public float SpeedAmount { get; }
    public float SlowDownAmount { get { return 1 - (slowDown / slowDown + 100); } }
    public IDefaultImplementable Set(Transform transform)
    {
        return this;
    }
    public void OnMove() { }
    public void SetSlowDown(float slowDown, float duration)
    {
        mono.StartCoroutine(HandleSlow(slowDown, duration));
    }
    public IEnumerator HandleSlow(float slowDown, float duration)
    {
        this.slowDown += slowDown;

        yield return new WaitForSeconds(duration);

        this.slowDown -= slowDown;
    }
}