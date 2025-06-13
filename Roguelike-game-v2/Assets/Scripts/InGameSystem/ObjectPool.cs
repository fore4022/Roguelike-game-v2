using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
public class ObjectPool
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();
    private Dictionary<string, List<GameObject>> poolingObjects = new();

    private Transform root;

    private const int maxWorkPerSec = 120;
    private const int defaultObjectCount = 50;

    private int coroutineCount = 0;

    public ObjectPool()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if(go == null)
        {
            go = new GameObject { name = "@ObjectPool" };

            root = go.transform;
        }
    }
    public int ScriptableObjectsCount { get { return scriptableObjects.Count; } }
    public int PoolingObjectsCount { get { return poolingObjects.Count; } }
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerSec / coroutineCount, 1); } }
    public void ActiveObject(string prefabName)
    {
        GetGameObject(prefabName).SetActive(true);
    }
    public void DisableObject(GameObject prefab)
    {
        prefab.SetActive(false);
    }
    public GameObject GetGameObject(string prefabName)
    {
        foreach(GameObject instance in poolingObjects[prefabName])
        {
            if(!instance.activeSelf)
            {
                return instance;
            }
        }

        return null;
    }
    public T GetScriptableObject<T>(string type) where T : ScriptableObject
    {
        if(scriptableObjects.ContainsKey(type))
        {
            return (T)scriptableObjects[type];
        }

        return null;
    }
    public void ReSetting()
    {
        foreach(List<GameObject> objList in poolingObjects.Values)
        {
            foreach(GameObject obj in objList)
            {
                if(obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
    public void Create(List<GameObject> prefabs, ScriptableObjectType type, int count)
    {
        foreach(GameObject prefab in prefabs)
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingInstance(prefab, count, type));
        }
    }
    public void Create(GameObject prefab, int count)
    {
        Util.GetMonoBehaviour().StartCoroutine(CreatingInstance(prefab, count));
    }
    private void CreateInstance(GameObject parent, GameObject prefab, int count, int instanceCount, ref GameObject[] array)
    {
        for(int i = 0; i < count; i++)
        {
            array[instanceCount + i] = Object.Instantiate(prefab, parent.transform);
            array[instanceCount + i].SetActive(false);
        }
    }
    private void CreateScriptableObject(ScriptableObjectType type, string key)
    {
        if(!scriptableObjects.ContainsKey(key))
        {
            ScriptableObject so = default;

            switch(type)
            {
                case ScriptableObjectType.Monster:
                    so = Util.LoadingToPath<ScriptableObject>($"Assets/SO/Monster/{Managers.UserData.data.StageName}/{key}.asset");

                    MonsterStat_SO monsterStatSO = so as MonsterStat_SO;

                    if(monsterStatSO.hasExtraObject && monsterStatSO.extraObject != null)
                    {
                        Create(monsterStatSO.extraObject, defaultObjectCount);
                    }
                    break;
                case ScriptableObjectType.Skill:
                    so = Util.LoadingToPath<ScriptableObject>($"Assets/SO/Skill/{key}.asset");
                    break;
            }
            
            scriptableObjects.Add(key, so);
        }
    }
    private IEnumerator CreatingInstance(GameObject prefab, int count, ScriptableObjectType type = ScriptableObjectType.None)
    {
        GameObject[] array = new GameObject[count];

        GameObject parent = GameObject.Find(prefab.name);

        if(parent == null)
        {
            parent = new GameObject { name = prefab.name };
        }

        yield return new WaitUntil(() => parent != null);

        int instanceCount = 0;
        int createCount;

        parent.transform.parent = root;

        coroutineCount++;

        while(instanceCount < count)
        {
            createCount = Mathf.Min(MaxWorkPerSec, count - instanceCount);

            CreateInstance(parent, prefab, createCount, instanceCount, ref array);

            instanceCount += createCount;

            yield return null;
        }

        coroutineCount--;

        if(type != ScriptableObjectType.None)
        {
            CreateScriptableObject(type, prefab.name);
        }

        yield return new WaitUntil(() => scriptableObjects.ContainsKey(prefab.name) == true);

        Util.GetMonoBehaviour().StartCoroutine(SetInstance(array, prefab.name));
    }
    private IEnumerator SetInstance(GameObject[] array, string key)
    {
        ScriptableObject so;

        int sum = 0;
        int count;
        int index;

        coroutineCount++;

        so = scriptableObjects[key];

        while(sum < array.Length)
        {
            count = MaxWorkPerSec;

            for(index = sum; index < Mathf.Min(sum + count, array.Length); index++)
            {
                array[index].GetComponent<IScriptableData>().SO = so;
            }

            sum += count;

            yield return null;
        }

        coroutineCount--;

        if(poolingObjects.ContainsKey(key))
        {
            poolingObjects[key].AddRange(array);
        }
        else
        {
            poolingObjects.Add(key, array.ToList());
        }
    }
}