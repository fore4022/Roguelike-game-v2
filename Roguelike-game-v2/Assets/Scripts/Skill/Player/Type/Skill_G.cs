using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 
/// </para>
/// 
/// </summary>
public class Skill_G : Skill
{
    [SerializeField]
    private float duration;

    public bool Finished { get { return true; } }
    public void Set()
    {
        gameObject.transform.position = EnemyDetection.GetRandomEnemyPosition();

        StartCoroutine(Attacking());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    public IEnumerator Attacking()
    {
        float totalTime = 0;

        while(totalTime == duration) // 
        {
            if(totalTime > duration)
            {
                totalTime = duration;
            }

            totalTime += Time.deltaTime;

            yield return true;
        }


    }
}