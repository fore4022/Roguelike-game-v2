using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Text;
using System.Collections;
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
            Managers.Scene.loadScene -= ClearPool;
            Managers.Scene.loadScene += ClearPool;

            go = new GameObject { name = "@ObjectPool" };

            root = go.transform;
        }
    }
    public int ScriptableObjectsCount { get { return scriptableObjects.Count; } }
    public int PoolingObjectsCount { get { return poolingObjects.Count; } }
    public void ClearPool()
    {
        if(scriptableObjects != null)
        {
            scriptableObjects = new();
        }

        if(poolingObjects != null)
        {
            poolingObjects = new();
        }

        Managers.Scene.loadScene -= ClearPool;
    }
    public void CreateInstance(List<GameObject> prefabs, int count, bool isSet = false)
    {
        Util.GetMonoBehaviour().StartCoroutine(CreateObjects(prefabs, count, isSet));
    }
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
    private List<GameObject> Instantiate(GameObject prefab, int count)
    {
        List<GameObject> queue = new();

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

            queue.Add(instance);
        }

        return queue;
    }
    private IEnumerator CreateObjects(List<GameObject> prefabs, int count, bool isSet)
    {
        List<GameObject> list;

        foreach(GameObject prefab in prefabs)
        {
            CreateScriptableObject(prefab.name);
        }

        foreach(GameObject prefab in prefabs)
        {
            if(poolingObjects.ContainsKey(prefab.name))
            {
                list = poolingObjects[prefab.name];

                foreach (GameObject instance in Instantiate(prefab, count))
                {
                    list.Add(instance);
                }

                poolingObjects[prefab.name] = list;
            }
            else
            {
                list = Instantiate(prefab, count);

                poolingObjects.Add(prefab.name, list);
            }
        }

        if(!isSet)
        {
            yield break;
        }

        yield return new WaitUntil(() => scriptableObjects.Count == prefabs.Count);

        foreach(GameObject prefab in prefabs)
        {
            
        }
    }
    private async Task CreateScriptableObject(string information)
    {
        if (!scriptableObjects.ContainsKey(information))
        {
            StringBuilder path = new StringBuilder(information);

            path.Append(so);

            ScriptableObject scriptableObject = await Util.LoadingToPath<ScriptableObject>(path.ToString());

            scriptableObjects.Add(information, scriptableObject);
        }
    }
}