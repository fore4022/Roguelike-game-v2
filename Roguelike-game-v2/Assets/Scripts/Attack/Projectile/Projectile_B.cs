using System.Collections;
using UnityEngine;
public class Projectile_B : Projectile, IProjectile
{
    [SerializeField]
    private Collider2D effectCollider;

    private bool isExplosion = false;

    public bool Finished { get { return isExplosion && anime.GetCurrentAnimatorStateInfo(0).IsName(so.projectile_Info.animationName); } }
    public void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        transform.rotation = Calculate.GetQuaternion(direction, so.baseRotation);
        effectCollider.enabled = !enable;
        isExplosion = false;

        anime.Play("default");

        moving = StartCoroutine(Moving());
    }
    public void SetCollider()
    {
        enable = !enable;
        isExplosion = !isExplosion;

        if(isExplosion)
        {
            effectCollider.enabled = true;
            defaultCollider.enabled = false;
        }
        else
        {
            effectCollider.enabled = false;
            defaultCollider.enabled = false;
        }
    }
    public void Enter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            if(go.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(this);
            }

            if(!isExplosion)
            {
                anime.Play(so.projectile_Info.animationName);

                StopCoroutine(moving);

                moving = null;
            }
        }
    }
    public IEnumerator Moving()
    {
        while (true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}