using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StageInformation", menuName = "Create New SO/Create New StageInformation_SO")]
public class StageInformation_SO : ScriptableObject
{
    public List<SpawnInformation_SO> spawnInformationList;

    public Sprite background;

    public float difficulty;
    public float spawnDelay;
    public float statScale;
}