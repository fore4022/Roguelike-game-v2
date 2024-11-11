using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Text;
public class ObjectPool
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();
    private Dictionary<string, List<GameObject>> poolingObjects = new();

    private Transform root;

    private const string so = "SO";

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
        string key;

        foreach (GameObject prefab in prefabs)
        {
            CreateInstance(prefab, count);
        }

        //

        foreach(GameObject prefab in prefabs)
        {
            
        }
    }
    private async Task<ScriptableObject> CreateScriptableObject(string key)
    {
        if (!scriptableObjects.ContainsKey(key))
        {
            StringBuilder path = new StringBuilder(key);

            path.Append(so);

            ScriptableObject scriptableObject = await Util.LoadingToPath<ScriptableObject>(path.ToString());

            scriptableObjects.Add(key, scriptableObject);

            return scriptableObject;
        }

        return scriptableObjects[key];
    }
    private async void CreateInstance(GameObject prefab, int count)
    {
        List<GameObject> list = new();

        GameObject parent = GameObject.Find(prefab.name);

        if (parent == null)
        {
            parent = new GameObject { name = prefab.name };

            parent.transform.parent = root;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject instance = Object.Instantiate(prefab, parent.transform);

            instance.SetActive(false);

            list.Add(instance);
        }

        SetInstance(list, prefab.name);
    }
    private async Task SetInstance(List<GameObject> prefabs, string key)
    {
        ScriptableObject so = await CreateScriptableObject(key);

        foreach (GameObject instance in prefabs)
        {
            IScriptableData scriptableData = instance.GetComponent<IScriptableData>();

            scriptableData.SetScriptableObject = so;
        }

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