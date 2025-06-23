using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> monsterList = new();

    private Dictionary<string, ScriptableObject> monsterStats = new();

    private const float minimumSpawnDelay = 0.075f;

    private int[] monsterSpawnProbabilityArray = new int[100];

    private Coroutine monsterSpawn = null;
    private Coroutine spawnGroup = null;
    private float spawnDelay = 0;

    private void Awake()
    {
        Managers.Game.monsterSpawner = this;
    }
    public void StartSpawn()
    {
        monsterSpawn = StartCoroutine(SpawningSystem());
    }
    public void ReStart()
    {
        if(spawnGroup != null)
        {
            StopCoroutine(spawnGroup);
        }

        StopCoroutine(monsterSpawn);
        StartSpawn();
    }
    private void LoadInformation()
    {
        foreach(GameObject monster in monsterList)
        {
            string soName = monster.name;

            if(!monsterStats.ContainsKey(soName))
            {
                monsterStats.Add(soName, Managers.Game.objectPool.GetScriptableObject<ScriptableObject>(soName));
            }
        }
    }
    private void MonsterSpawn(SpawnInformation_SO spawnInformation) 
    {
        int randomValue = Random.Range(0, 100);
        int arrayIndexValue = monsterSpawnProbabilityArray[randomValue];

        Managers.Game.objectPool.ActiveObject(spawnInformation.monsterInformation[arrayIndexValue].monster.name);
    }
    private IEnumerator SpawningSystem()
    {
        LoadInformation();

        while(!Managers.Game.IsGameOver)
        {
            foreach(SpawnInformation_SO spawnInformation in Managers.Game.stageInformation.spawnInformationList)
            {
                spawnGroup = StartCoroutine(MonsterSpawning(spawnInformation));

                yield return new WaitUntil(() => spawnGroup == null);
            }
        }

        StopCoroutine(spawnGroup);
    }
    private IEnumerator MonsterSpawning(SpawnInformation_SO spawnInformation)
    {
        int totalMinutes = Managers.Game.inGameTimer.GetTotalMinutes;
        int index = 0;

        spawnDelay = Managers.Game.difficultyScaler.SpawnDelay;

        foreach (SpawnInformation spawnInfo in spawnInformation.monsterInformation)
        {
            for (int i = 0; i < spawnInfo.spawnProbability; i++)
            {
                monsterSpawnProbabilityArray[i] = index;
            }

            index++;
        }

        while (Managers.Game.inGameTimer.GetTotalMinutes < totalMinutes + spawnInformation.duration)
        {
            if(spawnDelay != minimumSpawnDelay)
            {
                spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);
            }

            MonsterSpawn(spawnInformation);

            yield return new WaitForSeconds(1);
        }

        spawnGroup = null;
    }
}