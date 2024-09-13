using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, ScriptableObject> monsterStats = new();

    private const float minimumSpawnDelay = 0.075f;

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
        //foreach (SpawnInformation_SO info in Managers.Game.StageInformation.spawnInformationList)
        //{
        //    string soName = info.monster.name;

        //    monsterStats.Add(soName, ObjectPool.GetScriptableObject<ScriptableObject>(soName));
        //}

        //float spawnDelay;

        //while(true)
        //{
        //    spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);

        //    //ObjectPool.GetOrActiveObject();

        //    yield return new WaitForSeconds(spawnDelay);
        //}

        yield return null;
    }
}