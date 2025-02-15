using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnInformation
{
    public GameObject monster;

    public float spawnProbability;
}
[CreateAssetMenu(fileName = "SpawnInformation", menuName = "Create New SO/Create New SpawnInformation_SO")]
public class SpawnInformation_SO : ScriptableObject
{
    public List<SpawnInformation> monsterInformation;

    public int duration;
}