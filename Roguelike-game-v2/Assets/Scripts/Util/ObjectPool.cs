using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class ObjectPool
{
    public static Dictionary<string, ScriptableObject> scriptableObjects = new();
    public static Dictionary<string, List<GameObject>> poolingObjects = new();

    public static Transform root;

    public static string so = "SO";
    public const int CreateCount = 50;

    public static void Init()
    {

        GameObject go = GameObject.Find("@ObjectPool");

        if (go == null)
        {
            SceneManager.sceneLoaded -= ClearPool;
            SceneManager.sceneLoaded += ClearPool;

            go = new GameObject { name = "@ObjectPool" };

            root = go.transform;
        }
    }
    public static void ClearPool(Scene scene, LoadSceneMode mode)
    {
        if(scriptableObjects != null)
        {
            scriptableObjects = new();
        }

        if(poolingObjects != null)
        {
            poolingObjects = new();
        }
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
    public static List<GameObject> Instantiate(GameObject prefab, int count)
    {
        List<GameObject> queue = new();

        for (int i = 0; i < count; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab, root);

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
    public static async void CreateScriptableObject(string information)
    {
        await CreateInstanceScriptableObject(information);
    }
    public static T GetScriptableObject<T>(string soName) where T : ScriptableObject
    {
        if(scriptableObjects[soName] == null)
        {
            return null;
        }

        return (T)scriptableObjects[soName];
    }
    public static async Task CreateObjects(int count, List<GameObject> prefabs)
    {
        List<GameObject> queue;

        string soName;

        Init();

        foreach(GameObject prefab in prefabs)
        {
            soName = prefab.name + so;

            if(poolingObjects.ContainsKey(prefab.name))
            {
                queue = poolingObjects[prefab.name];

                foreach (GameObject instance in Instantiate(prefab, count))
                {
                    queue.Add(instance);
                }

                poolingObjects[prefab.name] = queue;
            }
            else
            {
                queue = Instantiate(prefab, count);

                poolingObjects.Add(prefab.name, queue);
            }

            await CreateInstanceScriptableObject(soName);
        }
    }
    public static async Task CreateObjects(int count, bool loadType, string information)//
    {
        List<GameObject> queue;

        string soName;

        Init();

        if (loadType)//Load To Lable
        {
            foreach (GameObject prefab in await Util.LoadToLable<GameObject>(information))
            {
                if (poolingObjects.ContainsKey(prefab.name))
                {
                    queue = poolingObjects[prefab.name];

                    foreach (GameObject instance in Instantiate(prefab, count))
                    {
                        queue.Add(instance);
                    }

                    poolingObjects[prefab.name] = queue;
                }
                else
                {
                    queue = Instantiate(prefab, count);

                    poolingObjects.Add(prefab.name, queue);
                }

                soName = prefab.name + so;

                await CreateInstanceScriptableObject(soName);
            }
        }
        else//Load To Path
        {
            GameObject prefab = await Util.LoadToPath<GameObject>(information);

            soName = prefab.name + so;

            if (poolingObjects.ContainsKey(prefab.name))
            {
                queue = poolingObjects[prefab.name];

                foreach (GameObject instance in Instantiate(prefab, count))
                {
                    queue.Add(instance);
                }

                poolingObjects[prefab.name] = queue;
            }
            else
            {
                queue = Instantiate(prefab, count);

                poolingObjects.Add(prefab.name, queue);
            }

            await CreateInstanceScriptableObject(soName);
        }
    }
    public static async Task CreateInstanceScriptableObject(string information)
    {
        if (!scriptableObjects.ContainsKey(information))
        {
            ScriptableObject so = await Util.LoadToPath<ScriptableObject>(information);

            scriptableObjects.Add(information, so);
        }
    }
}