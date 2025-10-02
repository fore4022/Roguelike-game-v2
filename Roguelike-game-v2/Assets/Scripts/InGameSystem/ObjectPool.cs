using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// <para>
/// 오브젝트 풀
/// </para>
/// 프리팹의 인스턴스에 대한 생성과 제어
/// </summary>
public class ObjectPool
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();
    private Dictionary<string, List<PoolingObject>> poolingObjects = new();

    private Transform root;

    private const int maxWorkPerSec = 180;
    private const int defaultObjectCount = 500;

    private int coroutineCount = 0;

    public ObjectPool()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if(go == null)
        {
            go = new GameObject { name = "@ObjectPool" };
        }

        root = go.transform;
    }
    public int ScriptableObjectsCount { get { return scriptableObjects.Count; } }
    public int PoolingObjectsCount { get { return poolingObjects.Count; } }
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerSec / coroutineCount, 1); } }
    public GameObject ActiveObject(string prefabName)
    {
        GameObject go = GetObject(prefabName, false).PoolingGameObject;

        go.SetActive(true);

        return go;
    }
    public void DisableObject(GameObject prefab, string key)
    {
        poolingObjects.TryGetValue(key, out List<PoolingObject> objs);

        objs.FirstOrDefault(o => o.PoolingGameObject == prefab).isUsed = false;

        prefab.SetActive(false);
    }
    public PoolingObject GetObject(string prefabName, bool isUsed = true)
    {
        foreach(PoolingObject obj in poolingObjects[prefabName])
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
    public T GetScriptableObject<T>(string type) where T : ScriptableObject
    {
        if(scriptableObjects.ContainsKey(type))
        {
            return (T)scriptableObjects[type];
        }

        return null;
    }
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
    public void Create(GameObject prefab, ScriptableObjectType type, int count = defaultObjectCount)
    {
        Util.GetMonoBehaviour().StartCoroutine(CreatingInstance(prefab, count, ScriptableObjectType.None));
    }
    public void Create(List<GameObject> prefabs, ScriptableObjectType type, int count = defaultObjectCount)
    {
        foreach(GameObject prefab in prefabs)
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingInstance(prefab, count, type));
        }
    }
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
    private void Create_Additional(GameObject prefab, string originalKey, int count)
    {
        if(prefab != null)
        {
            Util.GetMonoBehaviour().StartCoroutine(CreatingInstance(prefab, count, ScriptableObjectType.None, originalKey));
        }
    }
    private void CreateInstance(GameObject parent, GameObject prefab, int count, int instanceCount, ref GameObject[] array)
    {
        for(int i = 0; i < count; i++)
        {
            array[instanceCount + i] = Object.Instantiate(prefab, parent.transform);
            array[instanceCount + i].SetActive(false);
        }
    }
    private async void CreateScriptableObject(ScriptableObjectType type, string key)
    {
        if(!scriptableObjects.ContainsKey(key))
        {
            ScriptableObject so = default;

            switch(type)
            {
                case ScriptableObjectType.Monster:
                    so = await Util.LoadingToPath<ScriptableObject>($"Assets/SO/Monster/{Managers.UserData.data.StageName}/{key}.asset");

                    if(so is MonsterStat_WithObject_SO exceptionMonsterStatSO)
                    {
                        if(exceptionMonsterStatSO.extraObjects != null)
                        {
                            foreach(GameObject go in exceptionMonsterStatSO.extraObjects)
                            {
                                if(!poolingObjects.ContainsKey(go.name))
                                {
                                    Create_Additional(go, key, defaultObjectCount);
                                }
                            }
                        }
                    }
                    break;
                case ScriptableObjectType.Skill:
                    so = await Util.LoadingToPath<ScriptableObject>($"Assets/SO/Skill/{key}.asset");
                    break;
            }

            scriptableObjects.Add(key, so);
        }
    }
    private IEnumerator CreatingInstance(GameObject prefab, int count, ScriptableObjectType type = ScriptableObjectType.None, string originalKey = null)
    {
        GameObject[] array = new GameObject[count];

        string key = prefab.name;

        GameObject parent = GameObject.Find(key);

        if(parent == null)
        {
            parent = new GameObject { name = key };
        }

        yield return new WaitUntil(() => parent != null);

        int instanceCount = 0;
        int createCount;

        parent.transform.parent = root;

        coroutineCount++;

        while(instanceCount < count)
        {
            createCount = Mathf.Min(MaxWorkPerSec, count - instanceCount);

            CreateInstance(parent, prefab, createCount, instanceCount, ref array);

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
            CreateScriptableObject(type, key);

            yield return new WaitUntil(() => scriptableObjects.ContainsKey(key) == true);

            Util.GetMonoBehaviour().StartCoroutine(SetInstance(array, key));
        }

        coroutineCount--;
    }
    private IEnumerator SetInstance(GameObject[] array, string key)
    {
        ScriptableObject so;

        int sum = 0;
        int count;
        int index;

        coroutineCount++;

        so = scriptableObjects[key];

        while(sum < array.Length)
        {
            count = MaxWorkPerSec;

            for(index = sum; index < Mathf.Min(sum + count, array.Length); index++)
            {
                array[index].GetComponent<IScriptableData>().SO = so;
            }

            sum += count;

            yield return null;
        }

        coroutineCount--;
    }
}