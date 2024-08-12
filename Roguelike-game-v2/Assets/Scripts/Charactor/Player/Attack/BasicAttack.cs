using System.Collections;
using UnityEngine;
public class BasicAttack : MonoBehaviour, IDamage
{
    public float Damage { get { return Managers.Game.player.basicAttack.Damage; } }

    private Coroutine coroutine;
    private Collider2D col;

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
        if(Managers.Game.player.basicAttack.basicAttackSO != null)
        {
            yield return new WaitForSeconds(Managers.Game.player.basicAttack.basicAttackSO.duration);

            ObjectPool.DisableObject(this.gameObject, Managers.Game.player.basicAttack.basicAttackSO.attackType.name);
        }
    }
}