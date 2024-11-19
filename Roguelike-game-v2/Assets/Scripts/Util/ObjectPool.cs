using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Collections;
using System.Linq;
public class ObjectPool
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();
    private Dictionary<string, List<GameObject>> poolingObjects = new();

    private Transform root;

    private const string so = "SO";
    private const int maxWorkPerSec = 50;

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
    private void CreateInstance(GameObject parent, GameObject prefab, int count, int instanceCount, ref GameObject[] array)
    {
        GameObject instance;

        for (int i = 0; i < count; i++)
        {
            instance = Object.Instantiate(prefab, parent.transform);

            instance.SetActive(false);

            array[instanceCount + i] = (instance);
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
        GameObject[] array = new GameObject[count];

        GameObject parent = GameObject.Find(prefab.name);

        if(parent == null)
        {
            parent = new GameObject { name = prefab.name };
        }

        yield return new WaitUntil(() =>parent != null);

        string key = prefab.name;
        int instanceCount = 0;
        int createCount;

        parent.transform.parent = root;

        coroutineCount++;

        while (instanceCount < count)
        {
            createCount = Mathf.Min(MaxWorkPerSec, count - instanceCount);

            CreateInstance(parent, prefab, createCount, instanceCount, ref array);

            instanceCount += createCount;

            yield return null;
        }

        coroutineCount--;

        Util.GetMonoBehaviour().StartCoroutine(SetInstance(array, key));
    }
    private IEnumerator SetInstance(GameObject[] array, string key)
    {
        ScriptableObject so;

        int index = 0;
        int count;

        coroutineCount++;

        CreateScriptableObject(key);

        yield return new WaitUntil(() => scriptableObjects.ContainsKey(key) == true);

        so = scriptableObjects[key];

        while (index < array.Length)
        {
            count = MaxWorkPerSec;

            for (int i = index; i < Mathf.Min(index + count, array.Length); i++)
            {
                array[i].GetComponent<IScriptableData>().SetScriptableObject = so;
            }

            index += count;

            yield return null;
        }

        coroutineCount--;

        if (poolingObjects.ContainsKey(key))
        {
            poolingObjects[key].AddRange(array);
        }
        else
        {
            poolingObjects.Add(key, array.ToList());
        }

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = null;

            yield return null;
        }
    }
}