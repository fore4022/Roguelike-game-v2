using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class ObjectPool
{
    public static Dictionary<string, ScriptableObject> scriptableObjects = new();
    public static Dictionary<string, Queue<GameObject>> poolingObjects = new();

    public static Transform root;
    public static string so = "SO";

    public static void Init()
    {
        SceneManager.sceneLoaded += ClearPool;

        GameObject go = GameObject.Find("@ObjectPool");

        if (go == null)
        {
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
    public static async void CreateToLable(string lable, int count)
    {
        await CreateObjects(count, true, lable);
    }
    public static async void CreateToPath(string path, int count)
    {
        await CreateObjects(count, false, path);
    }
    public static GameObject FindObject(string prefabName)
    {
        if (!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = poolingObjects[prefabName].Peek();

        return prefab;
    }
    public static GameObject GetObject(string prefabName)
    {
        if(!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = poolingObjects[prefabName].Dequeue();

        return prefab;
    }
    public static GameObject GetOrActiveObject(string prefabName)
    {
        if(!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = poolingObjects[prefabName].Dequeue();

        prefab.SetActive(true);

        return prefab;
    }
    public static void ActiveObject(string prefabName)
    {
        GameObject prefab = poolingObjects[prefabName].Dequeue();

        prefab.SetActive(true);
    }
    public static void DisableObject(GameObject prefab, string prefabName)
    {
        prefab.SetActive(false);

        poolingObjects[prefabName].Enqueue(prefab);
    }
    public static Queue<GameObject> Instantiate(GameObject prefab, int count)
    {
        Queue<GameObject> queue = new();

        for (int i = 0; i < count; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab, root);

            instance.SetActive(false);

            queue.Enqueue(instance);
        }

        return queue;
    }
    public static async void CreateScriptableObjectToLable(string information)
    {
        await CreateInstanceScriptableObject(true, information);
    }
    public static async void CreateScriptableObjectToPath(string information)
    {
        await CreateInstanceScriptableObject(false, information);
    }
    public static T GetScriptableObject<T>(string soName) where T : ScriptableObject
    {
        if(scriptableObjects[soName] == null)
        {
            return null;
        }

        return (T)scriptableObjects[soName];
    }
    public static async Task CreateObjects(int count, bool loadType, string information)
    {
        Queue<GameObject> queue;

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
                        queue.Enqueue(instance);
                    }

                    poolingObjects[prefab.name] = queue;
                }
                else
                {
                    queue = Instantiate(prefab, count);

                    poolingObjects.Add(prefab.name, queue);
                }
            }
        }
        else//Load To Path
        {
            GameObject prefab = await Util.LoadToPath<GameObject>(information);

            if (poolingObjects.ContainsKey(prefab.name))
            {
                queue = poolingObjects[prefab.name];

                foreach (GameObject instance in Instantiate(prefab, count))
                {
                    queue.Enqueue(instance);
                }

                poolingObjects[prefab.name] = queue;
            }
            else
            {
                queue = Instantiate(prefab, count);

                poolingObjects.Add(prefab.name, queue);
            }
        }

        await CreateInstanceScriptableObject(loadType, information + so);
    }
    public static async Task CreateInstanceScriptableObject(bool loadType, string information)
    {
        if(loadType)//Load To Lable
        {
            foreach (ScriptableObject sobj in await Util.LoadToLable<ScriptableObject>("so"))
            {
                if(!scriptableObjects.ContainsKey(sobj.name + so))
                {
                    scriptableObjects.Add(sobj.name + so, sobj);
                }
            }
        }
        else//Load TO Path
        {
            if(!scriptableObjects.ContainsKey(information))
            {
                ScriptableObject sobj = await Util.LoadToPath<ScriptableObject>(information);

                scriptableObjects.Add(information, sobj);
            }
        }
    }
}