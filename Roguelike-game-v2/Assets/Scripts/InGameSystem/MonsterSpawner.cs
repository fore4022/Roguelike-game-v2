using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, ScriptableObject> monsterStats = new();

    private void Start()
    {
        Managers.Game.monsterSpawner = this;
    }
    public void Set()
    {
        StartCoroutine(MonsterSpawning());
    }
    private IEnumerator MonsterSpawning()
    {
        foreach (GameObject monster in Managers.Game.StageInformation.monsterList)
        {
            string soName = monster.name;

            monsterStats.Add(soName, ObjectPool.GetScriptableObject<ScriptableObject>(soName));
        }

        while(true)
        {
            yield return new WaitForSeconds(Managers.Game.difficultyScaler.SpawnDelay);
        }
    }
}