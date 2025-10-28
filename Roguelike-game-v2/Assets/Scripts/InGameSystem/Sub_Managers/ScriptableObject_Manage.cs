using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptableObject_Manage
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();

    private const int maxWorkPerFrame = 360;

    private int coroutineCount = 0;

    public Dictionary<string, ScriptableObject> ScriptableObjects { get { return scriptableObjects; } }
    public int ScriptableObjectsCount { get { return scriptableObjects.Count; } }
    private int MaxWorkPerSec { get { return Mathf.Max(maxWorkPerFrame / coroutineCount, 1); } }
    // Ű�� �ش��ϴ� ScriptableObject ��ȯ
    public T GetScriptableObject<T>(string key) where T : ScriptableObject
    {
        if(scriptableObjects.ContainsKey(key))
        {
            return (T)scriptableObjects[key];
        }

        return null;
    }
    // �ش� Ű�� SO �ҷ�����, ScriptableObjectType�� ���� ScriptableObject�� ������ �߰� ������Ʈ ����
    public async void LoadScriptableObject(ScriptableObjectType type, string key)
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
                                if(!Managers.Game.objectPool.PoolingObjects.ContainsKey(go.name))
                                {
                                    Managers.Game.objectPool.Create(go, key);
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
    // �Է� ���� �迭�� ��� ������Ʈ�� Ű�� �ش��ϴ� ScriptableObject�� �Ҵ�
    public IEnumerator SetScriptableObject(GameObject[] array, string key)
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