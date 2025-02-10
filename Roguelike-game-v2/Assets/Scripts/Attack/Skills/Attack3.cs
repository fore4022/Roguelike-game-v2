using UnityEngine;
public class Attack3 : Attack
{
    protected override void SetAttack()
    {
        Vector3 targetPosition = Managers.Game.enemyDetection.GetNearestEnemyPosition();

        transform.position = targetPosition;
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
