using UnityEngine;
public class Calculate
{
    public static Vector3 GetDirection(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition = Managers.Game.player.gameObject.transform.position).normalized;

        return direction;
    }
    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 position)
    {
        Vector3 direction = (targetPosition - position).normalized;

        return direction;
    }
}
