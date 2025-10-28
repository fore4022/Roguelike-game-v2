using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class ScriptableObject_Manage
{
    private Dictionary<string, ScriptableObject> scriptableObjects = new();

    private const int maxWorkPerFrame = 360;

    private int coroutineCount = 0;

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
    // �ش� Ű�� SO �ҷ�����, ScriptableObjectType�� ���� ScriptableObject�� ������ �߰� ������Ʈ ���� //
    public async Task LoadScriptableObject(ScriptableObjectType type, string key)
    {
        if(!scriptableObjects.ContainsKey(key))
        {
            ScriptableObject so = default;

            switch(type)
            {
                case ScriptableObjectType.Monster:
                    so = await AddressableHelper.LoadingToPath<ScriptableObject>($"Assets/SO/Monster/{Managers.Data.user.StageName}/{key}.asset");
                    break;
                case ScriptableObjectType.Skill:
                    so = await AddressableHelper.LoadingToPath<ScriptableObject>($"Assets/SO/Skill/{key}.asset");
                    break;
            }

            scriptableObjects.Add(key, so);
        }
    }
    // �Է� ���� �迭�� ��� ������Ʈ�� Ű�� �ش��ϴ� ScriptableObject�� �Ҵ�
    public IEnumerator SetScriptableObject(List<PoolingObject> list, string key)
    {
        ScriptableObject so;

        int sum = 0;
        int count;
        int index;

        coroutineCount++;

        so = scriptableObjects[key];

        while(sum < list.Count)
        {
            count = MaxWorkPerSec;

            for(index = sum; index < Mathf.Min(sum + count, list.Count); index++)
            {
                list[index].GetComponent<IScriptableData>().SO = so;
            }

            sum += count;

            yield return null;
        }

        coroutineCount--;
    }
}