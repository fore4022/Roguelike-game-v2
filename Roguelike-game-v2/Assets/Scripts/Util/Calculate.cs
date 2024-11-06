using System.Collections.Generic;
using UnityEngine;
public static class Calculate//
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
    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 position, bool isNormalized = true)
    {
        if (targetPosition == null)
        {
            return new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0);
        }

        if(isNormalized)
        {
            return (targetPosition - position).normalized;
        }
        else
        {
            return (targetPosition - position);
        }
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
    public static void IsInvisible(Collider2D targetCollider)
    {
        if (!targetCollider.gameObject.activeSelf)
        {
            return;
        }

        Collider2D target = targetCollider;

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!GeometryUtility.TestPlanesAABB(planes, target.bounds)) { Object.Destroy(targetCollider.gameObject); }
    }
    public static int[] GetRandomValues(int maxValue, int count)
    {
        List<int> valueList = new();
        int[] result = new int[count];

        for(int i = 0; i < maxValue; i++)
        {
            valueList.Add(i);
        }

        for(int i = 0; i < count; i++)
        {
            int index = Random.Range(0, maxValue - i);

            result[i] = valueList[index];

            valueList.RemoveAt(index);
        }

        return result;
    }
}