using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Game Stage/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    public List<SpawnInformation_SO> spawnInformationList;
    public SpawnMonsterList_SO spawnMonsterList;
    public AudioClip bgm;

    public float difficulty = 1;
    public float statScale = 1;
    public float spawnDelay;
    [Tooltip("Minute")]
    public int requiredTime;
}