using System.Collections.Generic;
using UnityEngine;
public static class EnemyDetection
{
    public static GameObject findNearestEnemy()
    {
        List<GameObject> gameObjectArray = findEnemiesOnScreen();

        GameObject targetObject = null;

        float minDistance = 0;

        foreach(GameObject go in gameObjectArray)
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

        return targetObject;
    }
    public static GameObject findLargestEnemyGroup()
    {
        List<GameObject> gameObjectArray = findEnemiesOnScreen();

        GameObject targetObject = null;

        foreach (GameObject go in gameObjectArray)
        {

        }

        return targetObject;
    }
    public static List<GameObject> findEnemiesOnScreen()
    {
        List<GameObject> resultArray = new List<GameObject>();
        Collider2D[] colliderArray = null;

        Vector2 cameraPosition = Camera.main.transform.position;

        float y = Camera.main.orthographicSize * 2;
        float x = y * Camera.main.aspect;

        Vector2 cameraSize = new Vector2(x, y);

        int count = Physics2D.OverlapBoxNonAlloc(cameraPosition, cameraSize, 0, colliderArray, 0);

        foreach(Collider2D col in colliderArray)
        {
            resultArray.Add(col.gameObject);
        }

        return resultArray;
    }
    public static float GetDistance(GameObject go, out float result)
    {
        float distance = 0;

        return result = distance;
    }
}