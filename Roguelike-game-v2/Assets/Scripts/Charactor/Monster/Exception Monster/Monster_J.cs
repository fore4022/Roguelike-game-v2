using System.Collections;
using UnityEngine;
public class Monster_J : BasicMonster
{
    [SerializeField]
    private float duration;

    private Coroutine behavior = null;
    private WaitForSeconds delay;

    protected override void Init()
    {
        delay = new(duration);

        base.Init();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return delay;

        Die();
    }
}