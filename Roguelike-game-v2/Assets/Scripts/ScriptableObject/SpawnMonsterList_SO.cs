using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnMonsterList", menuName = "Create New SO/Create New SpawnMonsterList_SO")]
public class SpawnMonsterList_SO : ScriptableObject
{
    public List<GameObject> monsters;
}