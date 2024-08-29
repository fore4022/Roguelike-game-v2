using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public static class ObjectPool
{
    public static Dictionary<string, Queue<GameObject>> poolingObjects = new();
    public static Transform root;

    public static void Init()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if (go == null)
        {
            go = new GameObject { name = "@ObjectPool" };

            root = go.transform;
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
    }
    public static GameObject GetObject(string prefabName)
    {
        if(!poolingObjects.ContainsKey(prefabName))
        {
            return null;
        }

        GameObject prefab = poolingObjects[prefabName].Peek();

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
}