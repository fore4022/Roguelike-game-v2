using System.Collections;
using UnityEngine;
public class Monster_N : Monster_WithObject
{
    [SerializeField]
    private Vector3 skillRotation;
    [SerializeField]
    private float skillDuration;
    [SerializeField]
    private float skillRange;
    [SerializeField]
    private float skillCooldown;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string skillKey;

    protected override void Init()
    {
        delay = new(skillDuration);
        skillKey = monsterSO.extraObjects[0].name;

        base.Init();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        transform.SetRotation(skillRotation, skillDuration, EaseType.InQuad);

        yield return delay;
    
        behavior = StartCoroutine(RepeatBehavior());
    }
}