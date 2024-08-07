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
    public static async Task CreateObjects(int count, string lable = null, string path = null)
    {
        if (lable == path) { return; }

        Queue<GameObject> queue;

        Init();

        if (path == null)
        {
            foreach (GameObject prefab in await Util.LoadToLable<GameObject>(lable))
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
        else
        {
            GameObject prefab = await Util.LoadToPath<GameObject>(path);

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
    public static GameObject GetOrActiveObject(string prefabName)
    {
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