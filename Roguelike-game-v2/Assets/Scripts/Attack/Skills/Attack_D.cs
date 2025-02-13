using System.Collections;
using UnityEngine;
public class Attack_D : Attack
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.enemyDetection.GetRandomVector();

        anime.Play("default");
    }
    protected override IEnumerator Attacking()
    {
        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        anime.Play("explosion");

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }
}