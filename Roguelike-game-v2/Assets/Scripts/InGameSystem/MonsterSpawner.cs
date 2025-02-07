using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, ScriptableObject> monsterStats = new();
    public List<GameObject> monsterList = new();

    private const float minimumSpawnDelay = 0.075f;

    private Coroutine monsterSpawn = null;
    private int[] monsterSpawnProbabilityArray = new int[100];
    private float spawnDelay = 0;

    private void Start()
    {
        Managers.Game.monsterSpawner = this;
    }
    public void StartSpawn()
    {
        StartCoroutine(SpawningSystem());
    }
    private void LoadInformation()
    {
        foreach(GameObject monster in monsterList)
        {
            string soName = monster.name;

            monsterStats.Add(soName, Managers.Game.inGameData.dataInit.objectPool.GetScriptableObject<ScriptableObject>(soName));
        }
    }
    private void MonsterSpawn(SpawnInformation_SO spawnInformation)
    {
        int randomValue = Random.Range(0, 100);
        int arrayIndexValue = monsterSpawnProbabilityArray[randomValue];

        Managers.Game.inGameData.dataInit.objectPool.ActiveObject(spawnInformation.monsterInformation[arrayIndexValue].monster.name);
    }
    private IEnumerator SpawningSystem()
    {
        LoadInformation();

        while(true)
        {
            foreach(SpawnInformation_SO spawnInformation in Managers.Game.stageInformation.spawnInformationList)
            {
                monsterSpawn = StartCoroutine(MonsterSpawning(spawnInformation));

                yield return new WaitUntil(() => monsterSpawn == null);
            }
        }
    }
    private IEnumerator MonsterSpawning(SpawnInformation_SO spawnInformation)
    {
        int totalMinutes = Managers.Game.inGameTimer.GetTotalMinutes;

        if(spawnInformation.monsterInformation.Count != 1)
        {
            int index = 0;

            foreach(SpawnInformation spawnInfo in spawnInformation.monsterInformation)
            {
                for(int i = 0; i < spawnInfo.spawnProbability; i++)
                {
                    monsterSpawnProbabilityArray[i] = index;
                }

                index++;
            }
        }

        while(Managers.Game.inGameTimer.GetTotalMinutes < totalMinutes + spawnInformation.duration)
        {
            if(spawnDelay != minimumSpawnDelay)
            {
                spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);
            }

            MonsterSpawn(spawnInformation);

            yield return new WaitForSeconds(spawnDelay);
        }

        monsterSpawn = null;
    }
}