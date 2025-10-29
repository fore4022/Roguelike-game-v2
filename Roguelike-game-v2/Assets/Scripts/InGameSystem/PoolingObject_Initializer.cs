using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class PoolingObject_Initializer
{
    private bool isInit = false;

    public bool Init { get { return isInit; } }
    public void Start(List<GameObject> monsterList, List<GameObject> skillList)
    {
        CoroutineHelper.Start(Initializing(monsterList, skillList));
    }
    private IEnumerator Initializing(List<GameObject> monsterList, List<GameObject> skillList)
    {
        yield return new WaitUntil(() => true);

        List<PoolingObject> objs;
        ScriptableObject so;

        string key;

        foreach(GameObject obj in monsterList)
        {
            key = obj.name;
            objs = Managers.Game.objectPool.GetObjects(key);

            Task load = Managers.Game.so_Manage.LoadScriptableObject(ScriptableObjectType.Monster, key);

            yield return new WaitUntil(() => load.IsCompleted);

            CoroutineHelper.Start(Managers.Game.so_Manage.SetScriptableObject(objs, key));

            so = Managers.Game.so_Manage.GetScriptableObject<ScriptableObject>(key);

            if(so is MonsterStat_WithObject_SO exceptionMonsterStatSO)
            {
                if(exceptionMonsterStatSO.extraObjects != null)
                {
                    foreach(GameObject extraObj in exceptionMonsterStatSO.extraObjects)
                    {
                        if(!Managers.Game.objectPool.PoolingObjects.ContainsKey(extraObj.name))
                        {
                            CoroutineHelper.Start(CreateAndSet_ExtraObject(extraObj, key));
                        }
                    }
                }
            }
        }

        foreach(GameObject obj in skillList)
        {
            key = obj.name;
            objs = Managers.Game.objectPool.GetObjects(key);

            Task load = Managers.Game.so_Manage.LoadScriptableObject(ScriptableObjectType.Skill, key);

            yield return new WaitUntil(() => load.IsCompleted);

            CoroutineHelper.Start(Managers.Game.so_Manage.SetScriptableObject(objs, key));
        }

        isInit = true;
    }
    private IEnumerator CreateAndSet_ExtraObject(GameObject extraObj, string key)
    {
        string key_extra = extraObj.name;

        Managers.Game.objectPool.Create(extraObj);

        yield return new WaitUntil(() => Managers.Game.objectPool.PoolingObjects.ContainsKey(key_extra));

        if(extraObj.GetComponent<MonsterSkill_Damage>())
        {
            Monster monster = Managers.Game.objectPool.GetObject(key, false).GetComponent<Monster>();
            MonsterSkill_Damage skillDamage;

            foreach(PoolingObject poolingObj in Managers.Game.objectPool.PoolingObjects[key_extra])
            {
                skillDamage = poolingObj.GetComponent<MonsterSkill_Damage>();
                skillDamage.Damage += monster.Damage;
            }
        }
    }
}