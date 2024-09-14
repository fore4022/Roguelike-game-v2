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
    private void SpawnMonster()
    {
        //Instantiate()

        //ObjectPool.GetObject();
    }
    private Vector2 GetSpawnPoint()
    {
        return new();
    }
    private IEnumerator MonsterSpawning()
    {
        LoadInformation();

        float spawnDelay = 0;
        int totalMinutes;

        while(true)
        {
            totalMinutes = Managers.Game.inGameTimer.GetTotalMinutes;

            foreach (SpawnInformation_SO spawnInformation in Managers.Game.StageInformation.spawnInformationList)
            {
                while(totalMinutes + spawnInformation.duration == Managers.Game.inGameTimer.GetTotalMinutes)
                {
                    if(spawnDelay != minimumSpawnDelay)
                    {
                        spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);
                    }

                    yield return new WaitForSeconds(spawnDelay);
                }
            }
        }
    }
}
/*

while (true)
        {
            spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);

            SpawnMonster();

            yield return new WaitForSeconds(spawnDelay);
        } 

*/