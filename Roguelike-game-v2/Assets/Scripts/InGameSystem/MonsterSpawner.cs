using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, ScriptableObject> monsterStats = new();
    private List<GameObject> monsterList;

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
        Managers.Game.GetMonsterList(ref monsterList);

        foreach (GameObject monster in monsterList)
        {
            string soName = monster.name;

            monsterStats.Add(soName, ObjectPool.GetScriptableObject<ScriptableObject>(soName));
        }
    }
    private void SpawnMonster()
    {
        //ObjectPool.GetObject();
    }
    private Vector2 GetSpawnPoint()
    {
        return new();
    }
    private IEnumerator MonsterSpawning()
    {
        LoadInformation();

        float spawnDelay;

        while (true)
        {
            spawnDelay = Mathf.Max(Managers.Game.difficultyScaler.SpawnDelay, minimumSpawnDelay);

            SpawnMonster();

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}