using System.Collections;
using UnityEngine;
/// <summary>
/// 의존성 주입에 사용되는 IMoveable의 구현
/// </summary>
public class DefaultMoveable : IMoveable, IDefaultImplementable
{
    private MonoBehaviour mono;

    private float slowDown = 0;

    public float SpeedAmount { get; }
    public float SlowDownAmount
    {
        get
        {
            if(slowDown == 0)
            {
                return 1;
            }

            return 1 - (slowDown / (slowDown + 100));
        }
    }
    public IDefaultImplementable Set(Transform transform)
    {
        mono = transform.GetComponent<MonoBehaviour>();

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