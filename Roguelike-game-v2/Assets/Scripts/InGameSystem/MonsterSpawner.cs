using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 몬스터를 주기적으로 생성
/// </summary>
public class MonsterSpawner
{
    [HideInInspector]
    public List<GameObject> monsterList = new();

    private Dictionary<string, ScriptableObject> monsterStats = new();

    private const float minimumSpawnDelay = 0.05f;

    private Coroutine monsterSpawn = null;
    private Coroutine spawnGroup = null;
    private int[] monsterSpawnProbabilityArray = new int[100];
    private float spawnDelay = 0;

    public MonsterSpawner()
    {
        Managers.Game.monsterSpawner = this;
    }
    public void StartSpawn()
    {
        monsterSpawn = CoroutineHelper.StartCoroutine(SpawningSystem());
    }
    public void StopSpawn()
    {
        CoroutineHelper.StopCoroutine(spawnGroup);
    }
    public void ReStart()
    {
        if(spawnGroup != null)
        {
            CoroutineHelper.StopCoroutine(spawnGroup);
        }

        CoroutineHelper.StopCoroutine(monsterSpawn);
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
        int arrayIndexValue = monsterSpawnProbabilityArray[Random.Range(0, 100)];

        Managers.Game.objectPool.ActiveObject(spawnInformation.monsterInformation[arrayIndexValue].monster.name);
    }
    private IEnumerator SpawningSystem()
    {
        LoadInformation();

        while(!Managers.Game.GameOver)
        {
            foreach(SpawnInformation_SO spawnInformation in Managers.Game.stageInformation.spawnInformationList)
            {
                spawnGroup = CoroutineHelper.StartCoroutine(MonsterSpawning(spawnInformation));

                yield return new WaitUntil(() => spawnGroup == null);
            }
        }

        CoroutineHelper.StopCoroutine(spawnGroup);
    }
    private IEnumerator MonsterSpawning(SpawnInformation_SO spawnInformation)
    {
        int totalMinutes = Managers.Game.inGameTimer.GetTotalMinutes;
        int index = 0;

        foreach(Spawn_Information spawnInfo in spawnInformation.monsterInformation)
        {
            for(int i = 0; i < spawnInfo.spawnProbability; i++)
            {
                monsterSpawnProbabilityArray[i] = index;
            }

            index++;
        }

        spawnDelay = Managers.Game.difficultyScaler.SpawnDelay;

        while(Managers.Game.inGameTimer.GetTotalMinutes < totalMinutes + spawnInformation.duration)
        {
            if(spawnDelay != minimumSpawnDelay)
            {
                spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);
            }

            MonsterSpawn(spawnInformation);

            yield return new WaitForSeconds(spawnDelay);
        }

        spawnGroup = null;
    }
}