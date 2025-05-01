using System.Collections.Generic;
using UnityEngine;
public static class Calculate
{
    public static float width = Util.CameraWidth / 2 - offset;
    public static float height = Util.CameraHeight / 2 - offset;

    private static Vector3 vec = new();
    private static float offset = 0.5f;

    public static float GetParabolicY(float size, float peakValue, float value)
    {
        if(peakValue == 0 || size == 0)
        {
            return 0;
        }

        return peakValue * (Mathf.Pow((value - size / 2), 2) / Mathf.Pow(size / 2, 2));
    }
    public static Vector3 GetAttackPosition(Vector3 direction, float attackRange)
    {
        return direction * attackRange + Managers.Game.player.transform.position;
    }
    public static Vector3 GetDirection(Vector3 targetPosition)
    {
        if (targetPosition == null)
        {
            return GetRandomDirection();
        }

        return (targetPosition - Managers.Game.player.gameObject.transform.position).normalized;
    }
    public static Vector3 GetDirection(Vector3 targetPosition, Vector3 position, bool isNormalized = true)
    {
        if(targetPosition == null || position == null || targetPosition == position)
        {
            return GetRandomDirection();
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
    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(0, 361), Random.Range(0, 361), 0).normalized;
    }
    public static Quaternion GetQuaternion(Vector3 direction)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    public static Quaternion GetQuaternion(Vector3 direction, Vector3 baseRotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + baseRotation.z);
    }
    public static Vector2 GetRandomVector()
    {
        vec.x = Random.Range(-width, width);
        vec.y = Random.Range(-height, height);

        return vec;
    }
    public static int[] GetRandomValues(int maxValue, int count = 0)
    {
        List<int> valueList = new();
        int[] result;
        int index;

        for (int i = 0; i < maxValue; i++)
        {
            valueList.Add(i);
        }

        if(count != 0)
        {
            result = new int[count];

            for (int i = 0; i < count; i++)
            {
                index = Random.Range(0, maxValue - i);
                result[i] = valueList[index];

                valueList.RemoveAt(index);
            }
        }
        else
        {
            result = new int[maxValue];

            for (int i = 0; i < maxValue; i++)
            {
                index = Random.Range(0, maxValue - i);
                result[i] = valueList[index];

                valueList.RemoveAt(index);
            }
        }

        return result;
    }
}