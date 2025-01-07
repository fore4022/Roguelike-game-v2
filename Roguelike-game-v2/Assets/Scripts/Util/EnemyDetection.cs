using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class EnemyDetection
{
    public float largeastRange = 2.5f;
    public int maximumEnemyCount = 15;

    private float width = Util.CameraWidth / 2;
    private float height = Util.CameraHeight / 2;

    public GameObject FindNearestEnemy(GameObject center = null, float? range = null)
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen(center, range);

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
    public GameObject FindLargestEnemyGroup()
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
    public List<GameObject> FindLargestEnemyGroup(int count)
    {
        List<GameObject> gameObjectList = FindEnemiesOnScreen();
        List<(GameObject obj, int enemyCount) > targetObjectList = new List<(GameObject obj, int enemyCount)>();
        Collider2D[] colliderArray = new Collider2D[maximumEnemyCount];

        int enemyCount;

        foreach(GameObject go in gameObjectList)
        {
            enemyCount = Physics2D.OverlapCircleNonAlloc(go.transform.position, largeastRange, colliderArray);

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
    public Vector2 GetNearestEnemyPosition(GameObject center = null, float? range = null)
    {
        GameObject target = FindNearestEnemy(center, range);

        if (target == null)
        {
            return GetRandomVector();
        }
        else
        {
            return target.transform.position;
        }
    }
    public Vector2 GetLargestEnemyGroup()
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
    public List<Vector2> GetLargestEnemyGroup(int count)
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

            foreach (GameObject target in targetList)
            {
                targetPositionList.Add(target.transform.position);
            }
        }

        return targetPositionList;
    }
    public List<GameObject> FindEnemiesOnScreen(GameObject center = null, float? range = null)
    {
        List<GameObject> resultList = new List<GameObject>();
        Collider2D[] colliderArray;
        
        Vector2 radiusPosition;

        if(center == null)
        {
            radiusPosition = Camera.main.transform.position;
        }
        else
        {
            radiusPosition = center.transform.position;
        }

        if(range == null)
        {
            float y = Camera.main.orthographicSize * 2;
            float x = y * Camera.main.aspect;

            Vector2 radius = Managers.Game.calculate.GetVector(x, y);

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
    public float GetDistance(GameObject go, out float result)
    {
        float distance = (go.transform.position - Managers.Game.player.gameObject.transform.position).magnitude;

        return result = distance;
    }
    public Vector2 GetRandomVector()
    {
        float x = Random.Range(-width, width);
        float y = Random.Range(-height, height);

        return new Vector3(x, y) + Managers.Game.player.gameObject.transform.position;
    }
}