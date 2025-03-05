using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    public List<SpawnInformation_SO> spawnInformationList;

    public GameObject background;

    public float difficulty = 1;
    public float statScale = 1;
    public float spawnDelay;
    [Tooltip("Minute")]
    public int requiredTime;
}