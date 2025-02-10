using System.Collections;
public abstract class Projectile : Attack
{
    protected abstract override IEnumerator StartAttack();
    protected abstract override IEnumerator Attacking();
    protected abstract IEnumerator Moving();
}