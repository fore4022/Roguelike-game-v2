using System.Collections;
using UnityEngine;
public class BasicAttack : MonoBehaviour, IDamage
{
    public BasicAttack_SO basicAttackSO;

    private Coroutine coroutine;
    private Collider2D col;

    public float Damage { get { return Managers.Game.player.Stat.damage * basicAttackSO.damageCoefficient; } }
    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        Vector3 targetPosition = EnemyDetection.FindNearestEnemy().transform.position;
        Vector3 direction = Calculate.GetDirection(targetPosition);
        Vector3 localSize = Calculate.GetSize(basicAttackSO.attackSize);
        Quaternion quaternion = Calculate.GetQuaternion(direction);

        gameObject.transform.position = Calculate.GetAttackPosition(direction, basicAttackSO.attackRange);
        gameObject.transform.rotation = quaternion;
        gameObject.transform.localScale = localSize;

        coroutine = StartCoroutine(Disable());
    }
    private void OnDisable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    private void Update()
    {
        Util.IsInvisible(col);
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
        yield return new WaitUntil(() => basicAttackSO != null);

        yield return new WaitForSeconds(basicAttackSO.duration);

        ObjectPool.DisableObject(this.gameObject, basicAttackSO.attackType.name);
    }
}