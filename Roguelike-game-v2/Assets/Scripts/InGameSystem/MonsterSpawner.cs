using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, ScriptableObject> monsterStats = new();

    private Coroutine monsterSpawn = null;

    private const float minimumSpawnDelay = 0.075f;

    private float spawnDelay = 0;
    private int totalMinutes;

    private void Start()
    {
        Managers.Game.monsterSpawner = this;
    }
    public void Set()
    {
        StartCoroutine(SpawningSystem());
    }
    private void LoadInformation()
    {
        List<GameObject> monsterList = new();

        Managers.Game.GetMonsterList(ref monsterList);

        foreach (GameObject monster in monsterList)
        {
            string soName = monster.name;

            monsterStats.Add(soName, ObjectPool.GetScriptableObject<ScriptableObject>(soName));
        }
    }
    private void MonsterSpawn()
    {
        Instantiate();
    }
    private Vector2 GetSpawnPoint()
    {
        return new();
    }
    private IEnumerator SpawningSystem()
    {
        LoadInformation();


        while(true)
        {
            totalMinutes = Managers.Game.inGameTimer.GetTotalMinutes;

            foreach (SpawnInformation_SO spawnInformation in Managers.Game.StageInformation.spawnInformationList)
            {
                monsterSpawn = StartCoroutine(MonsterSpawning(spawnInformation));

                yield return new WaitUntil(() => monsterSpawn == null);
            }
        }
    }
    private IEnumerator MonsterSpawning(SpawnInformation_SO spawnInformation)
    {
        while (totalMinutes + spawnInformation.duration == Managers.Game.inGameTimer.GetTotalMinutes)
        {
            if (spawnDelay != minimumSpawnDelay)
            {
                spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);
            }

            MonsterSpawn();

            yield return new WaitForSeconds(spawnDelay);
        }

        monsterSpawn = null;
    }
}