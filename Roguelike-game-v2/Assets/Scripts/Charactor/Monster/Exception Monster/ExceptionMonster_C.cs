using System.Collections;
using UnityEngine;
public class ExceptionMonster_C : ExceptionMonster
{
    [SerializeField]
    private float dashSpeed;

    private bool hasDashedToPlayer = false;

    private Coroutine behavior = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Die()
    {
        
    }
    private IEnumerator RepeatBehavior()
    {
        yield return null;

        speedMultiplier = 1;
    }
}