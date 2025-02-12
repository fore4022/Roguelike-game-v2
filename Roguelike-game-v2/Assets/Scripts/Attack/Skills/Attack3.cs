using UnityEngine;
public class Attack3 : Attack
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.enemyDetection.GetNearestEnemyPosition();
    }
    protected override void Enter(GameObject go)
    {
        base.Enter(go);

        if(go.TryGetComponent(out Monster monster))
        {
            //monster.
        }
    }
}
