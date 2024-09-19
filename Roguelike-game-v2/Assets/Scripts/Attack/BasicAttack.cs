using System.Collections;
using UnityEngine;
public abstract class BasicAttack : MonoBehaviour, IDamage
{
    protected Attack_SO basicAttackSO;

    protected Coroutine coroutine;

    public float Damage { get { return Managers.Game.player.PlayerStat.damage * basicAttackSO.damageCoefficient; } }
    protected virtual void OnEnable()
    {
        coroutine = StartCoroutine(Disable());
    }
    protected void OnDisable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.GetDamage(this);
        }
    }
    private IEnumerator Disable()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();

        render.enabled = false;

        basicAttackSO = ObjectPool.GetScriptableObject<Attack_SO>(name);

        yield return new WaitUntil(() => basicAttackSO != null);

        Set();
        
        render.enabled = true;

        yield return new WaitForSeconds(basicAttackSO.duration);

        basicAttackSO = null;
        coroutine = null;

        ObjectPool.DisableObject(this.gameObject);
    }
    protected abstract void Set();
}