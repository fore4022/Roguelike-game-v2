using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterSpawner : MonoBehaviour
{
    private Dictionary<string, Stat_SO> monsterStats = new();

    private void Start()
    {
        
    }
    private IEnumerator MonsterSpawning()
    {


        //yield return new WaitUntil();

        while(true)
        {
            yield return null;
            //yield return new WaitForSeconds();
        }
    }
}
