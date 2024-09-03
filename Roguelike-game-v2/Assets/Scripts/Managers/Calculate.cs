using UnityEngine;
public class Calculate
{
    public static Vector3 GetAttackPosition(Vector3 direction, float attackRange)
    {
        return direction * attackRange + Managers.Game.player.transform.position;
    }
    public static Vector3 GetDirection(Vector3 targetPosition)
    {
        if (targetPosition == null)
        {
            return new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0);
        }

        return (targetPosition - Managers.Game.player.gameObject.transform.position).normalized;
    }
    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 position)
    {
        if (targetPosition == null)
        {
            return new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0);
        }

        return (targetPosition - position).normalized;
    }
    public static Vector3 GetVector(float vec)
    {
        return new Vector3(vec, vec, 0);
    }
    public static Vector3 GetVector(float vecX, float vecY)
    {
        return new Vector3(vecX, vecY, 0);
    }
    public static Quaternion GetQuaternion(Vector3 direction)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}