using System.Collections.Generic;
using UnityEngine;
public class Calculate
{
    public float GetParabolicY(float size, float peakValue, float value)
    {
        if(peakValue == 0 || size == 0)
        {
            return 0;
        }

        return peakValue * (Mathf.Pow((value - size / 2), 2) / Mathf.Pow(size / 2, 2));
    }
    public Vector3 GetAttackPosition(Vector3 direction, float attackRange)
    {
        return direction * attackRange + Managers.Game.player.transform.position;
    }
    public Vector3 GetDirection(Vector3 targetPosition)
    {
        if (targetPosition == null)
        {
            return GetRandomDirection();
        }

        return (targetPosition - Managers.Game.player.gameObject.transform.position).normalized;
    }
    public Vector3 GetDirection(Vector3 targetPosition, Vector3 position, bool isNormalized = true)
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
    public Vector3 GetVector(float vec)
    {
        return new Vector3(vec, vec, 0);
    }
    public Vector3 GetVector(float vecX, float vecY)
    {
        return new Vector3(vecX, vecY, 0);
    }
    public Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(0, 361), Random.Range(0, 361), 0).normalized;
    }
    public Quaternion GetQuaternion(Vector3 direction)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    public int[] GetRandomValues(int maxValue, int count)
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