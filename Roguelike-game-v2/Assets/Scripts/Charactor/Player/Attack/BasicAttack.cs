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
        yield return new WaitUntil(() => basicAttackSO != null);

        yield return new WaitForSeconds(basicAttackSO.duration);

        ObjectPool.DisableObject(this.gameObject, basicAttackSO.attackType.name);
    }
}