using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Collections;
public class ObjectPool
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();
    private Dictionary<string, List<GameObject>> poolingObjects = new();

    private Transform root;

    private const string so = "SO";
    private const int maxWorkPerSec = 20;

    private int coroutineCount = 0;

    public ObjectPool()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if (go == null)
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
        GameObject prefab = GetActiveGameObject(prefabName);
        
        prefab.SetActive(true);
    }
    public void DisableObject(GameObject prefab)
    {
        prefab.SetActive(false);
    }
    public GameObject GetActiveGameObject(string prefabName)
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
    public void CreateObjects(List<GameObject> prefabs, int count)
    {
        foreach(GameObject prefab in prefabs)
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingInstance(prefab, count));
        }
    }
    private void CreateInstance(GameObject parent, GameObject prefab, int count, ref List<GameObject> list)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject instance = Object.Instantiate(prefab, parent.transform);

            instance.SetActive(false);

            list.Add(instance);
        }
    }
    private async void CreateScriptableObject(string key)
    {
        if (!scriptableObjects.ContainsKey(key))
        {
            StringBuilder path = new StringBuilder(key);

            path.Append(so);

            ScriptableObject scriptableObject = await Util.LoadingToPath<ScriptableObject>(path.ToString());

            scriptableObjects.Add(key, scriptableObject);
        }
    }
    private IEnumerator CreatingInstance(GameObject prefab, int count)
    {
        List<GameObject> list = new();

        GameObject parent = new GameObject { name = prefab.name };

        string key = prefab.name;
        int instanceCount = 0;
        int createCount;

        parent.transform.parent = root;

        coroutineCount++;


        while (instanceCount < count)
        {
            createCount = Mathf.Min(MaxWorkPerSec, count - instanceCount);

            instanceCount += createCount;

            CreateInstance(parent, prefab, createCount, ref list);

            yield return null;
        }

        coroutineCount--;

        Util.GetMonoBehaviour().StartCoroutine(SetInstance(list, key));
    }
    private IEnumerator SetInstance(List<GameObject> prefabs, string key)
    {
        ScriptableObject so;

        int index = 0;
        int count;

        coroutineCount++;

        CreateScriptableObject(key);

        yield return new WaitUntil(() => scriptableObjects.ContainsKey(key) == true);

        so = scriptableObjects[key];

        while (index < prefabs.Count)
        {
            count = maxWorkPerSec;

            for (int i = index; i < count; i++)
            {
                prefabs[i].GetComponent<IScriptableData>().SetScriptableObject = so;
            }

            index += count;

            yield return null;
        }

        coroutineCount--;

        if (poolingObjects.ContainsKey(key))
        {
            poolingObjects[key].AddRange(prefabs);
        }
        else
        {
            poolingObjects.Add(key, prefabs);
        }
    }
}