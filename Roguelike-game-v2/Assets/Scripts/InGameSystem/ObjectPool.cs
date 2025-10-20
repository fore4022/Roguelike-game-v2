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

    private const int maxWorkPerSec = 240;
    private const int defaultObjectCount = 500;

    private int coroutineCount = 0;

    // 오브젝트 풀이 생성될 때, 풀링 되는 객체들이 위치할 root 
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
    // 초당 작업량 반환
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerSec / coroutineCount, 1); } }
    // 키에 해당하는 오브젝트 활성화
    public GameObject ActiveObject(string prefabKey)
    {
        GameObject go = GetObject(prefabKey, false).PoolingGameObject;

        go.SetActive(true);

        return go;
    }
    // 키에 해당하는 리스트에서, 입력 받은 오브젝트를 가지는 PoolingObject isUsed와 오브젝트 비활성화
    public void DisableObject(GameObject prefab, string key)
    {
        poolingObjects.TryGetValue(key, out List<PoolingObject> objs);

        objs.Find(o => o.PoolingGameObject == prefab).isUsed = false;

        prefab.SetActive(false);
    }
    // 키에 해당하는 오브젝트 반환, 활성화 여부 지정 가능
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
    // 키에 해당하는 오브젝트 전부 반환
    public List<PoolingObject> GetObjects(string prefabKey)
    {
        if(poolingObjects.ContainsKey(prefabKey))
        {
            return poolingObjects[prefabKey];
        }

        return null;
    }
    // 키에 해당하는 ScriptableObject 반환
    public T GetScriptableObject<T>(string key) where T : ScriptableObject
    {
        if(scriptableObjects.ContainsKey(key))
        {
            return (T)scriptableObjects[key];
        }

        return null;
    }
    // 모든 PoolingObject를 초기 상태로 설정,isUsed와 오브젝트 비활성화
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
    // 프리팹을 개수만큼 생성
    public void Create(GameObject prefab, int count = defaultObjectCount)
    {
        CoroutineHelper.StartCoroutine(CreatingInstance(prefab, count, ScriptableObjectType.None, null, false));
    }
    // 프리팹 항목들을 개수만큼 생성, ScriptableObjectType에 따라서 프리팹 항목들에 해당하는 ScriptableObject 불러오기
    public void Create(List<GameObject> prefabs, ScriptableObjectType type, int count = defaultObjectCount)
    {
        foreach(GameObject prefab in prefabs)
        {
            CoroutineHelper.StartCoroutine(CreatingInstance(prefab, count, type));
        }
    }
    // 오브젝트 풀로 생성된 오든 오브젝트의 코루틴 중단
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
    // 키에 해당하는 리스트에 프리팹을 개수만큼 추가 생성
    private void Create_Additional(GameObject prefab, string originalKey, int count)
    {
        if(prefab != null)
        {
            CoroutineHelper.StartCoroutine(CreatingInstance(prefab, count, ScriptableObjectType.None, originalKey));
        }
    }
    // 프리팹을 _root의 자식으로 개수만큼 생성
    private void CreateInstance(Transform _root, GameObject prefab, int count, int instanceCount, ref GameObject[] array)
    {
        for(int i = 0; i < count; i++)
        {
            array[instanceCount + i] = Object.Instantiate(prefab, _root);
            array[instanceCount + i].SetActive(false);
        }
    }
    // 해당 키의 SO 불러오기, ScriptableObjectType에 따라서 ScriptableObject가 가지는 추가 오브젝트 생성
    private async void CreateScriptableObject(ScriptableObjectType type, string key)
    {
        if(!scriptableObjects.ContainsKey(key))
        {
            ScriptableObject so = default;

            switch(type)
            {
                case ScriptableObjectType.Monster:
                    so = await AddressableHelper.LoadingToPath<ScriptableObject>($"Assets/SO/Monster/{Managers.Data.user.StageName}/{key}.asset");

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
                    so = await AddressableHelper.LoadingToPath<ScriptableObject>($"Assets/SO/Skill/{key}.asset");
                    break;
            }

            scriptableObjects.Add(key, so);
        }
    }
    // 프레임마다 프리팹 인스턴스 생성, 목표 개수만큼 생성된 이후, ScriptableObjectType에 따라서 ScriptableObject를 할당해준다.
    private IEnumerator CreatingInstance(GameObject prefab, int count, ScriptableObjectType type = ScriptableObjectType.None, string originalKey = null, bool isSetRoot = true)
    {
        GameObject[] array = new GameObject[count];

        string key = prefab.name;

        GameObject parent = GameObject.Find(key);
        Transform transform;

        if(parent == null)
        {
            transform = new GameObject { name = key }.transform;
        }
        else
        {
            transform = parent.transform;
        }

        yield return new WaitUntil(() => parent != null);

        int instanceCount = 0;
        int createCount;

        if(isSetRoot)
        {
            parent.transform.parent = root;
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
            CreateScriptableObject(type, key);

            yield return new WaitUntil(() => scriptableObjects.ContainsKey(key) == true);

            CoroutineHelper.StartCoroutine(SetScriptableObject(array, key));
        }

        coroutineCount--;
    }
    // 입력 받은 배열의 모든 오브젝트에 키에 해당하는 ScriptableObject를 할당
    private IEnumerator SetScriptableObject(GameObject[] array, string key)
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