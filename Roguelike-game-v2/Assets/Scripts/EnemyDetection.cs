using System.Collections.Generic;
using UnityEngine;
public static class EnemyDetection
{
    public static float findLargeastRadius = 1.5f;

    public static GameObject findNearestEnemy()
    {
        List<GameObject> gameObjectList = findEnemiesOnScreen();

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
    public static GameObject findLargestEnemyGroup()
    {
        List<GameObject> gameObjectList = findEnemiesOnScreen();
        Collider2D[] colliderArray = null;

        GameObject targetObject = null;

        int maxIntCount = 0;

        foreach (GameObject go in gameObjectList)
        {
            int count = Physics2D.OverlapCircleNonAlloc(go.transform.position, findLargeastRadius, colliderArray);

            if(maxIntCount < count)
            {
                maxIntCount = count;
            }
        }

        if(maxIntCount == 0)
        {
            return null;
        }

        return targetObject;
    }
    public static List<GameObject> findEnemiesOnScreen()
    {
        List<GameObject> resultList = new List<GameObject>();
        Collider2D[] colliderArray = null;

        Vector2 cameraPosition = Camera.main.transform.position;

        float y = Camera.main.orthographicSize * 2;
        float x = y * Camera.main.aspect;

        Vector2 cameraSize = new Vector2(x, y);

        int count = Physics2D.OverlapBoxNonAlloc(cameraPosition, cameraSize, 0, colliderArray, 0);//monster layer

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