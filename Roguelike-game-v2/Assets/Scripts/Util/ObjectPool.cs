using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Text;
public static class ObjectPool
{
    public static Dictionary<string, ScriptableObject> scriptableObjects = new();
    public static Dictionary<string, List<GameObject>> poolingObjects = new();

    public static Transform root;

    public const string so = "SO";
    public const string clone = "(Clone)";
    public const int CreateCount = 50;

    public static void Init()
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
    public static void ClearPool()
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
    public static async void CreateToLable(string lable, int count = CreateCount)
    {
        await CreateObjects(count, true, lable);
    }
    public static async void CreateToPath(string path, int count = CreateCount)
    {
        await CreateObjects(count, false, path);
    }
    public static async void CreateInstance(List<GameObject> prefabs, int count = CreateCount)
    {
        await CreateObjects(count, prefabs);
    }
    public static GameObject FindObject(string prefabName)
    {
        if (!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = GetActiveGameObject(prefabName);

        return prefab;
    }
    public static GameObject GetObject(string prefabName)
    {
        if(!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = GetActiveGameObject(prefabName);

        return prefab;
    }
    public static GameObject GetOrActiveObject(string prefabName)
    {
        if(!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = GetActiveGameObject(prefabName);

        prefab.SetActive(true);

        return prefab;
    }
    public static void ActiveObject(string prefabName)
    {
        GameObject prefab = GetActiveGameObject(prefabName);

        prefab.SetActive(true);
    }
    public static void DisableObject(GameObject prefab)
    {
        prefab.SetActive(false);
    }
    public static List<GameObject> Instantiate(GameObject prefab, int count)//
    {
        List<GameObject> queue = new();

        GameObject parent = GameObject.Find(prefab.name);

        if(parent == null)
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
    public static GameObject GetActiveGameObject(string prefabName)
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
    public static T GetScriptableObject<T>(string type) where T : ScriptableObject
    {
        if(scriptableObjects.ContainsKey(type))
        {
            return (T)scriptableObjects[type];
        }

        return null;
    }
    public static async void CreateScriptableObject(string information)
    {
        await CreateInstanceScriptableObject(information);
    }
    public static async Task CreateObjects(int count, List<GameObject> prefabs)
    {
        List<GameObject> list;

        Init();

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

            await CreateInstanceScriptableObject(prefab.name);
        }
    }
    public static async Task CreateObjects(int count, bool loadType, string information)//
    {
        List<GameObject> list;

        Init();

        if (loadType)//Load To Lable
        {
            foreach (GameObject prefab in await Util.LoadingToLable<GameObject>(information))
            {
                if (poolingObjects.ContainsKey(prefab.name))
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

                await CreateInstanceScriptableObject(prefab.name);
            }
        }
        else//Load To Path
        {
            GameObject prefab = await Util.LoadingToPath<GameObject>(information);

            if (poolingObjects.ContainsKey(prefab.name))
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

            await CreateInstanceScriptableObject(prefab.name);
        }
    }
    public static async Task CreateInstanceScriptableObject(string information)
    {
        if (!scriptableObjects.ContainsKey(information))
        {
            StringBuilder path = new StringBuilder(information);
            StringBuilder key = new StringBuilder(information);

            path.Append(so);
            key.Append(clone);

            ScriptableObject scriptableObject = await Util.LoadingToPath<ScriptableObject>(path.ToString());

            scriptableObjects.Add(key.ToString(), scriptableObject);
        }
    }
}