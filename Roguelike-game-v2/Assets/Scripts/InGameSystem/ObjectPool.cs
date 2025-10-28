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
    public GameObject ActiveObject(string prefabKey)
    {
        GameObject go = GetObject(prefabKey, false).PoolingGameObject;

        go.SetActive(true);

        return go;
    }
    // Ű�� �ش��ϴ� ����Ʈ����, �Է� ���� ������Ʈ�� ������ PoolingObject isUsed�� ������Ʈ ��Ȱ��ȭ
    public void DisableObject(GameObject prefab, string key)
    {
        poolingObjects.TryGetValue(key, out List<PoolingObject> objs);

        objs.Find(o => o.PoolingGameObject == prefab).isUsed = false;

        prefab.SetActive(false);
    }
    // Ű�� �ش��ϴ� ������Ʈ ��ȯ, Ȱ��ȭ ���� ���� ����
    public PoolingObject GetObject(string prefabKey, bool isUsed = true)
    {
        foreach(PoolingObject obj in poolingObjects[prefabKey])
        {
            if(!obj.PoolingGameObject.activeSelf && !obj.isUsed)
            {
                if(isUsed)
                {
                    obj.isUsed = true;
                }

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
                if(obj.PoolingGameObject.activeSelf)
                {
                    obj.PoolingGameObject.SetActive(false);
                }

                if(obj.isUsed)
                {
                    obj.isUsed = false;
                }
            }
        }
    }
    // �������� ������ŭ ����
    public void Create(GameObject prefab, int count = defaultObjectCount)
    {
        CoroutineHelper.StartCoroutine(CreatingInstance(prefab, count, ScriptableObjectType.None, null, false));
    }
    // ������ �׸���� ������ŭ ����, ScriptableObjectType�� ���� ������ �׸�鿡 �ش��ϴ� ScriptableObject �ҷ�����
    public void Create(List<GameObject> prefabs, ScriptableObjectType type, int count = defaultObjectCount)
    {
        foreach(GameObject prefab in prefabs)
        {
            CoroutineHelper.StartCoroutine(CreatingInstance(prefab, count, type));
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
    // Ű�� �ش��ϴ� ����Ʈ�� �������� ������ŭ �߰� ����
    public void Create(GameObject prefab, string originalKey, int count = defaultObjectCount)
    {
        if(prefab != null)
        {
            CoroutineHelper.StartCoroutine(CreatingInstance(prefab, count, ScriptableObjectType.None, originalKey));
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
    // �����Ӹ��� ������ �ν��Ͻ� ����, ��ǥ ������ŭ ������ ����, ScriptableObjectType�� ���� ScriptableObject�� �Ҵ����ش�.
    private IEnumerator CreatingInstance(GameObject prefab, int count, ScriptableObjectType type = ScriptableObjectType.None, string originalKey = null, bool isSetRoot = true)
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

        if(type == ScriptableObjectType.None)
        {
            if(originalKey != null)
            {
                Monster monster = GetObject(originalKey, false).GetComponent<Monster>();

                if(array[0].GetComponent<MonsterSkill_Damage>())
                {
                    MonsterSkill_Damage skillDamage;

                    foreach(GameObject go in array)
                    {
                        skillDamage = go.GetComponent<MonsterSkill_Damage>();
                        skillDamage.Damage += monster.Damage;
                    }
                }
            }
        }
        else
        {
            Managers.Game.so_Manage.LoadScriptableObject(type, key);

            yield return new WaitUntil(() => Managers.Game.so_Manage.ScriptableObjects.ContainsKey(key) == true);

            CoroutineHelper.StartCoroutine(Managers.Game.so_Manage.SetScriptableObject(array, key));
        }

        coroutineCount--;
    }
}