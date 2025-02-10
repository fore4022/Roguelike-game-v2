using System.Collections;
using UnityEngine;
public abstract class Projectile : Attack
{
    protected Vector2 direction;                                                                                                                 

    protected abstract override IEnumerator StartAttack();
    protected abstract override IEnumerator Attacking();
    protected abstract IEnumerator Moving();
}