using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class EnemyDetection
{
    public static float largeastRange = 2.5f;

    private static Vector2 vec = new();

    public static Vector2 GetNearestEnemyPosition(Transform transform = null)
    {
        GameObject target = FindNearestEnemy(transform);

        if(target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public static Vector2 GetRandomEnemyPosition()
    {
        GameObject target = FindRandomEnemy();

        if(target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public static Vector2 GetLargestEnemyGroup()
    {
        GameObject target = FindLargestEnemyGroup();

        if(target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public static List<Vector2> GetLargestEnemyGroup(int count)
    {
        List<GameObject> targetList = FindLargestEnemyGroup(count);
        List<Vector2> targetPositionList = new List<Vector2>();

        if(targetList.Count == 0)
        {
            for(int i = 0; i < count; i++)
            {
                targetPositionList.Add(GetRandomVector());
            }
        }
        else
        {
            if(targetList.Count > count)
            {
                count -= targetList.Count;

                for(int i = 0; i < count; i++)
                {
                    targetPositionList.Add(GetRandomVector());
                }
            }

            foreach(GameObject target in targetList)
            {
                targetPositionList.Add(target.transform.position);
            }
        }

        return targetPositionList;
    }
    public static Vector2 GetRandomVector()
    {
        vec.x = Random.Range(-Calculate.width, Calculate.width);
        vec.y = Random.Range(-Calculate.height, Calculate.height);

        return vec + (Vector2)Managers.Game.player.gameObject.transform.position;
    }
    private static List<GameObject> FindLargestEnemyGroup(int count)
    {
        List<(GameObject obj, int enemyCount) > targetObjectList = new List<(GameObject obj, int enemyCount)>();
        List<GameObject> gameObjectList = FindEnemiesOnScreen();

        int enemyCount;

        foreach(GameObject go in gameObjectList)
        {
            enemyCount = Physics2D.OverlapCircleAll(go.transform.position, largeastRange).Count();

            targetObjectList.Add((go, enemyCount));
        }

        targetObjectList.OrderByDescending(tuple => tuple.enemyCount).ToList();

        gameObjectList = new List<GameObject>();

        foreach((GameObject obj, int enemyCount) value in targetObjectList)
        {
            gameObjectList.Add(value.obj);
        }

        return gameObjectList.Take(count).ToList();
    }
    private static List<GameObject> FindEnemiesOnScreen(float? range = null)
    {
        List<GameObject> resultList = new List<GameObject>();
        Collider2D[] colliderArray;
        
        Vector2 radiusPosition = Camera.main.transform.position;

        if(range == null)
        {
            float y = Camera.main.orthographicSize * 2;
            float x = y * Camera.main.aspect;

            Vector2 radius = new(x, y);

            colliderArray = Physics2D.OverlapBoxAll(radiusPosition, radius, 0, LayerMask.GetMask("Monster"));
        }
        else
        {
            colliderArray = Physics2D.OverlapCircleAll(radiusPosition, (float)range, 0, LayerMask.GetMask("Monster"));
        }

        foreach(Collider2D col in colliderArray)
        {
            resultList.Add(col.gameObject);
        }

        return resultList;
    }
    private static GameObject FindRandomEnemy()
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen();

        if(gameObjectList.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, gameObjectList.Count);

        return gameObjectList[index];
    }
    private static GameObject FindNearestEnemy(Transform transform = null)
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen();

        GameObject targetObject = null;

        float minDistance = 0;

        foreach(GameObject go in gameObjectList)
        {
            if(targetObject == null)
            {
                GetDistance(go, out minDistance, transform);

                targetObject = go;
            }
            else if(minDistance > GetDistance(go, out float distance, transform))
            {
                minDistance = distance;
                targetObject = go;
            }
        }

        if(minDistance == 0)
        {
            return null;
        }

        return targetObject;
    }
    private static GameObject FindLargestEnemyGroup()
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen();
        Collider2D[] colliderArray;

        GameObject targetObject = null;

        int maxIntCount = 0;
        int count;

        foreach(GameObject go in gameObjectList)
        {
            colliderArray = Physics2D.OverlapCircleAll(go.transform.position, largeastRange);
            count = colliderArray.Length;

            if(maxIntCount < count)
            {
                maxIntCount = count;
                targetObject = go;
            }
        }

        if(maxIntCount == 0)
        {
            return null;
        }

        return targetObject;
    }
    private static float GetDistance(GameObject go, out float result, Transform transform = null)
    {
        float distance = 0;

        if(transform == null)
        {
            distance = (go.transform.position - Managers.Game.player.gameObject.transform.position).magnitude;
        }
        else
        {
            distance = (go.transform.position - transform.position).magnitude;
        }

        return result = distance;
    }
}