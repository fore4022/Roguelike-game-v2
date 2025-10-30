using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// <para>
/// ������Ʈ Ǯ
/// </para>
/// �������� �ν��Ͻ��� ���� ������ ����
/// </summary>
public class ObjectPool
{
    private Dictionary<string, List<PoolingObject>> poolingObjects = new();

    private Transform root;

    private const int maxWorkPerFrame = 360;
    private const int defaultObjectCount = 500;

    private int coroutineCount = 0;

    // ������Ʈ Ǯ�� ������ ��, Ǯ�� �Ǵ� ��ü���� ��ġ�� root 
    public ObjectPool()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if(go == null)
        {
            go = new GameObject { name = "@ObjectPool" };
        }

        root = go.transform;
    }
    public Dictionary<string, List<PoolingObject>> PoolingObjects { get { return poolingObjects; } }
    public int PoolingObjectsCount { get { return poolingObjects.Count; } }
    // �����Ӵ� ������ ��ȯ
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerFrame / coroutineCount, 1); } }
    // Ű�� �ش��ϴ� ������Ʈ Ȱ��ȭ
    public PoolingObject ActiveObject(string prefabKey)
    {
        PoolingObject go = GetObject(prefabKey, false);

        go.SetActive(true);

        return go;
    }
    // Ű�� �ش��ϴ� ����Ʈ����, �Է� ���� ������Ʈ�� ������ PoolingObject isUsed�� ������Ʈ ��Ȱ��ȭ
    public void DisableObject(GameObject prefab, string key)
    {
        poolingObjects.TryGetValue(key, out List<PoolingObject> objs);

        objs.Find(o => o.GameObject == prefab).isInUse = false;

        prefab.SetActive(false);
    }
    // Ű�� �ش��ϴ� ������Ʈ ��ȯ, Ȱ��ȭ ���� ���� ����
    public PoolingObject GetObject(string prefabKey, bool setInUse = true)
    {
        foreach(PoolingObject obj in poolingObjects[prefabKey])
        {
            if(!obj.GameObject.activeSelf && (!obj.isInUse || obj.isUsed))
            {
                if(setInUse)
                {
                    obj.isInUse = true;
                }

                obj.isUsed = false;

                return obj;
            }
        }

        return null;
    }
    // Ű�� �ش��ϴ� ������Ʈ ���� ��ȯ
    public List<PoolingObject> GetObjects(string prefabKey)
    {
        if(poolingObjects.ContainsKey(prefabKey))
        {
            return poolingObjects[prefabKey];
        }

        return null;
    }
    // ��� PoolingObject�� �ʱ� ���·� ����,isUsed�� ������Ʈ ��Ȱ��ȭ
    public void ReSetting()
    {
        foreach(List<PoolingObject> objList in poolingObjects.Values)
        {
            foreach(PoolingObject obj in objList)
            {
                if(obj.GameObject.activeSelf)
                {
                    obj.GameObject.SetActive(false);
                }

                if(obj.isInUse)
                {
                    obj.isInUse = false;
                }
            }
        }
    }
    // �������� ������ŭ ����
    public void Create(GameObject prefab, int count = defaultObjectCount)
    {
        CoroutineHelper.Start(CreatingInstance(prefab, count, false));
    }
    // ������ �׸���� ������ŭ ����, ScriptableObjectType�� ���� ������ �׸�鿡 �ش��ϴ� ScriptableObject �ҷ�����
    public void Create(List<GameObject> prefabs, int count = defaultObjectCount)
    {
        foreach(GameObject prefab in prefabs)
        {
            CoroutineHelper.Start(CreatingInstance(prefab, count));
        }
    }
    // ������Ʈ Ǯ�� ������ ���� ������Ʈ�� �ڷ�ƾ �ߴ�
    public void StopAllActions()
    {
        foreach(List<PoolingObject> objs in poolingObjects.Values)
        {
            foreach(PoolingObject obj in objs)
            {
                if(obj.activeSelf)
                {
                    obj.StopAllCoroutines();
                }
            }
        }
    }
    // �������� _root�� �ڽ����� ������ŭ ����
    private void CreateInstance(Transform _root, GameObject prefab, int count, int instanceCount, ref GameObject[] array)
    {
        for(int i = 0; i < count; i++)
        {
            array[instanceCount + i] = Object.Instantiate(prefab, _root);
            array[instanceCount + i].SetActive(false);
        }
    }
    // ScriptableObjectType�� ���� ScriptableObject�� �Ҵ����ش�.
    private IEnumerator CreatingInstance(GameObject prefab, int count, bool isSetRoot = true)
    {
        GameObject[] array = new GameObject[count];

        string key = prefab.name;

        GameObject parent = GameObject.Find(key);
        Transform transform = null;

        if(parent == null)
        {
            transform = new GameObject { name = key }.transform;
        }
        else
        {
            transform = parent.transform;
        }

        yield return new WaitUntil(() => transform != null);

        int instanceCount = 0;
        int createCount;

        if(isSetRoot)
        {
            transform.SetParent(root);
        }

        coroutineCount++;

        while(instanceCount < count)
        {
            createCount = Mathf.Min(MaxWorkPerSec, count - instanceCount);

            CreateInstance(transform, prefab, createCount, instanceCount, ref array);

            instanceCount += createCount;

            yield return null;
        }

        if(!poolingObjects.ContainsKey(key))
        {
            poolingObjects.Add(key, new());
        }

        for(int i = 0; i < array.Count(); i++)
        {
            poolingObjects[key].Add(new(array[i]));
        }

        coroutineCount--;
    }
}