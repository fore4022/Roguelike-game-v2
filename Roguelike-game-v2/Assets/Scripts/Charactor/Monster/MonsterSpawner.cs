using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, Stat_SO> monsterStats = new();

    private void Start()
    {
        StartCoroutine(MonsterSpawning());
    }
    private IEnumerator MonsterSpawning()
    {
        //load stats

        yield return new WaitUntil(() => Managers.Game.StageInformation.monsterList.Count() == monsterStats.Count());

        while(true)
        {
            yield return null;
            //yield return new WaitForSeconds();
        }
    }
}
