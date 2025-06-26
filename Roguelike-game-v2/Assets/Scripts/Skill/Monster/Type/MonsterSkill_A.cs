using System.Collections;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MonsterSkill_A : MonsterSkillBase
{
    [SerializeField, Min(0.1f)]
    private float speed = 1;

    private Vector3 direction;

    protected override void Enable()
    {
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        gameObject.SetActive(false);
    }
    protected override void SetActive(bool isActive)
    {
        col.enabled = isActive;

        base.SetActive(isActive);
    }
    private IEnumerator Casting()
    {
        direction = Calculate.GetDirection(Managers.Game.player.transform.position, transform.position);
        transform.rotation = Calculate.GetQuaternion(direction);

        SetActive(true);

        while(true)
        {
            transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }
    }
}