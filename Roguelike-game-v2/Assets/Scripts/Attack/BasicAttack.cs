using System.Collections;
using UnityEngine;
public abstract class BasicAttack : MonoBehaviour, IDamage
{
    protected Attack_SO basicAttackSO;

    protected SpriteRenderer render;
    protected Coroutine attackCoroutine;

    public float DamageAmount { get { return Managers.Game.player.PlayerStat.damage * basicAttackSO.damageCoefficient; } }
    protected virtual void OnEnable()
    {
        attackCoroutine = StartCoroutine(Attacking());
    }
    protected void OnDisable()
    {
        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.GetDamage(this);
        }
    }
    private IEnumerator Attacking()
    {
        render = GetComponent<SpriteRenderer>();

        render.enabled = false;

        basicAttackSO = ObjectPool.GetScriptableObject<Attack_SO>(name);

        yield return new WaitUntil(() => basicAttackSO != null);

        Set();
        
        render.enabled = true;

        yield return new WaitForSeconds(basicAttackSO.duration);

        basicAttackSO = null;
        render = null;
        attackCoroutine = null;

        ObjectPool.DisableObject(this.gameObject);
    }
    protected abstract void Set();
}