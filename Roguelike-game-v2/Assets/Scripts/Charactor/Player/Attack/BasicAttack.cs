using System.Collections;
using UnityEngine;
public class BasicAttack : MonoBehaviour, IDamage
{
    public Attack_SO basicAttackSO;

    private Coroutine coroutine;

    public float Damage { get { return Managers.Game.player.DefaultStat.damage * basicAttackSO.damageCoefficient; } }
    private void OnEnable()
    {
        coroutine = StartCoroutine(Disable());
    }
    private void OnDisable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IDamageReceiver>(out IDamageReceiver damageReceiver))
        {
            damageReceiver.GetDamage(this);
        }
    }
    private IEnumerator Disable()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();

        render.enabled = false;

        yield return new WaitUntil(() => basicAttackSO != null);

        Vector3 targetPosition = EnemyDetection.GetNearestEnemyPosition();
        Vector3 direction = Calculate.GetDirection(targetPosition);
        Vector3 localSize = Calculate.GetVector(basicAttackSO.attackSize);
        Quaternion quaternion = Calculate.GetQuaternion(direction);

        gameObject.transform.position = Calculate.GetAttackPosition(direction, basicAttackSO.attackRange);
        gameObject.transform.rotation = quaternion;
        gameObject.transform.localScale = localSize;

        render.enabled = true;

        yield return new WaitForSeconds(basicAttackSO.duration);

        ObjectPool.DisableObject(this.gameObject, basicAttackSO.attackType.name);
    }
}