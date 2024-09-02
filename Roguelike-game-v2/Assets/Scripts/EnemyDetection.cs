using System.Collections.Generic;
using UnityEngine;
public static class EnemyDetection
{
    public static float largeastRange = 3.5f;
    public static int maximumEnemyCount = 15;

    public static GameObject FindNearestEnemy()
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen();

        GameObject targetObject = null;

        float minDistance = 0;

        foreach(GameObject go in gameObjectList)
        {
            if(targetObject == null)
            {
                GetDistance(go, out minDistance);

                targetObject = go;
            }
            else if(minDistance > GetDistance(go, out float distance))
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
    public static GameObject FindLargestEnemyGroup()
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen();
        Collider2D[] colliderArray = new Collider2D[maximumEnemyCount];

        GameObject targetObject = null;

        int maxIntCount = 0;

        foreach (GameObject go in gameObjectList)
        {
            int count = Physics2D.OverlapCircleNonAlloc(go.transform.position, largeastRange, colliderArray);

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
    public static List<GameObject> FindEnemiesOnScreen()
    {
        List<GameObject> resultList = new List<GameObject>();
        Collider2D[] colliderArray;

        float y = Camera.main.orthographicSize * 2;
        float x = y * Camera.main.aspect;

        Vector2 cameraSize = new Vector2(x, y);

        Vector2 cameraPosition = Camera.main.transform.position;

        colliderArray = Physics2D.OverlapBoxAll(cameraPosition, cameraSize, 0, LayerMask.GetMask("Monster"));

        foreach(Collider2D col in colliderArray)
        {
            resultList.Add(col.gameObject);
        }

        return resultList;
    }
    public static float GetDistance(GameObject go, out float result)
    {
        float distance = (go.transform.position - Managers.Game.player.gameObject.transform.position).magnitude;

        return result = distance;
    }
}